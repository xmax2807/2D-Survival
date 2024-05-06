#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
namespace Project.GameEventSystem.EventGraph.Editor
{
    public class EventGraphEditor : UnityEditor.EditorWindow
    {
        [UnityEditor.MenuItem("Event/EventGraph")]
        private static void ShowWindow()
        {
            var window = GetWindow<EventGraphEditor>();
            window.titleContent = new GUIContent("EventGraph");
            window.Show();
        }

        IEventDataSource m_activeSource;
        EventGraphConfig Config => m_activeSource.Config;
        private EventGraphSearchWindow m_searchWindow;
        private EventGraphView graph;
        private EventNodeBuilderFactory NodeFactory => Config.NodeBuilderFactory;
        IManipulator[] manipulators;

        void OnEnable()
        {
            //only show graph when selecting object that attached Event Trigger Source component
            Selection.selectionChanged += Init;
            EditorApplication.playModeStateChanged += PlayModeStateChanged;
            Init();
        }

        void OnDisable()
        {
            rootVisualElement.Clear();
            Selection.selectionChanged -= Init;
            EditorApplication.playModeStateChanged -= PlayModeStateChanged;

            if (m_searchWindow != null)
            {
                m_searchWindow.selectEntryEvent -= OnSelectEntry;
                Debug.Log("Unregistered");
            }
        }

        void Init()
        {
            rootVisualElement.Clear();
            if (m_searchWindow != null)
            {
                m_searchWindow.selectEntryEvent -= OnSelectEntry;
                Debug.Log("Unregistered");
            }

            // bool selectCondition = Selection.activeGameObject == null 
            // || !Selection.activeGameObject.TryGetComponent<EventTriggerSource>(out var triggerSource)
            // || Selection.activeObject != null;
            IEventDataSource source = Selection.activeObject as IEventDataSource;
            bool selectCondition = source != null;
            if (false == selectCondition)
            {
                ShowEmptyText(message: "No Event Trigger Source selected or can't edit in prefab stage");
                return;
            }


            if (this.manipulators == null)
            {
                manipulators = new IManipulator[]{
                    new ContentDragger(),
                    new SelectionDragger(),
                    new RectangleSelector(),
                    new ContentZoomer()
                };
            }

            if (m_searchWindow == null)
            {
                m_searchWindow = CreateInstance<EventGraphSearchWindow>();
            }

            if (m_searchWindow != null)
            {
                m_searchWindow.selectEntryEvent += OnSelectEntry;
                Debug.Log("Registered");
            }

            if (graph == null)
            {
                graph = new EventGraphView
                {
                    name = "EventGraph",
                    graphViewChanged = OnGraphViewChanged,
                    nodeCreationRequest = OnNodeCreationRequest
                };
                graph.StretchToParentSize();
            }


            ChangeDataSource(source);

            rootVisualElement.Add(graph);
            rootVisualElement.Add(BuildToolbar());
        }

        private Toolbar BuildToolbar()
        {
            var toolbar = new Toolbar();
            // Drag mode
            var toggle = new Toggle("Drag Mode");
            toggle.RegisterValueChangedCallback(ToggleDragMode);
            toolbar.Add(toggle);
            //

            return toolbar;
        }

        private void ToggleDragMode(ChangeEvent<bool> changeEvent)
        {
            bool newValue = changeEvent.newValue;
            if (newValue == false)
            {
                graph.RemoveManipulator(manipulators[0]);
                graph.RemoveManipulator(manipulators[1]);
            }
            else
            {
                graph.AddManipulator(manipulators[0]);
                graph.AddManipulator(manipulators[1]);
            }
        }

        private void OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context)
        {
            string sourcePath = m_activeSource.AssetPath;
            string folderPath = sourcePath[..sourcePath.LastIndexOf('/')];

            EventNodeData node = m_activeSource.NodeDataCreator.CreateAsset((int)entry.userData, location: folderPath);

            //calculate position
            Vector2 worldPos = this.rootVisualElement.ChangeCoordinatesTo(
                this.rootVisualElement.parent, context.screenMousePosition - this.position.position);
            Vector2 position = graph.contentViewContainer.WorldToLocal(worldPos);

            node.Id = Guid.NewGuid();
            node.NodePosition = position;
            EditorUtility.SetDirty(node);
            AssetDatabase.SaveAssets();

            OnNodeDataCreated(node);
        }

        readonly Dictionary<string, EventNode> cachedNodes = new();
        private void RecreateGraph(IEventDataSource source)
        {
            graph.ClearGraph();
            cachedNodes.Clear();

            if (source.Nodes.Count > 0)
            {
                CreateNodes(source.Nodes, cachedNodes);
            }
            if (source.Links.Count > 0)
            {
                CreateLinks(source.Links, cachedNodes);
            }
        }

        private void CreateLinks(IList<EventLinkData> links, in Dictionary<string, EventNode> cachedNodes)
        {
            foreach (var node in cachedNodes.Values)
            {
                foreach (VisualElement element in node.outputContainer.Children())
                {
                    Port port = element.Q<Port>();
                    if (port == null) continue;

                    // find all links that connect from this node to other nodes
                    var linkData = links.Where(x => x.OutputId == node.id && x.PortName == port.portName).ToArray();

                    foreach (var link in linkData)
                    {
                        if (!cachedNodes.TryGetValue(link.InputId.ToString(), out EventNode targetNode))
                        {
                            continue;
                        }
                        graph.Link(from: port, to: (Port)targetNode.inputContainer[0]);
                    }
                }
            }
        }

        private void CreateNodes(IEnumerable<EventNodeData> nodes, in Dictionary<string, EventNode> cachedNodes)
        {
            foreach (var nodeData in nodes)
            {
                if (nodeData == null) continue;
                EventNode node = NodeFactory.Build(nodeData);
                graph.AddNode(node, nodeData.NodePosition);
                cachedNodes.Add(node.id.ToString(), node);
            }
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.edgesToCreate != null)
            {
                foreach (var edge in graphViewChange.edgesToCreate)
                {
                    EventNode outputNode = edge.output.node as EventNode;
                    EventNode inputNode = edge.input.node as EventNode;
                    string portName = edge.output.portName;

                    EventLinkData linkData = EventLinkData.CreateInEditor(outputNode.id, inputNode.id, portName);
                    m_activeSource.AddLink(linkData);
                }
            }

            if (graphViewChange.elementsToRemove != null)
            {
                foreach (var element in graphViewChange.elementsToRemove)
                {
                    if (element is EventNode node)
                    {
                        EventNodeData nodeData = m_activeSource[node.id];
                        if (nodeData != null)
                        {
                            m_activeSource.NodeDataCreator.RemoveAsset(nodeData);
                            m_activeSource.RemoveNode(node.id);
                        }
                    }
                    else if (element is Edge edge)
                    {
                        Guid inputId = ((EventNode)edge.input.node).id;
                        Guid outputId = ((EventNode)edge.output.node).id;
                        string portName = edge.output.portName;
                        m_activeSource.RemoveLink(portName, inputId, outputId);
                    }
                }
            }

            if (graphViewChange.movedElements != null)
            {
                foreach (var movedElement in graphViewChange.movedElements)
                {
                    if (movedElement is EventNode node)
                    {
                        var nodeData = m_activeSource[node.id];
                        if(nodeData != null){
                            nodeData.NodePosition = node.GetPosition().position;
                        }
                    }
                }
            }

            m_activeSource.SaveChange();
            return graphViewChange;
        }

        private void OnNodeDataCreated(EventNodeData data)
        {
            if (graph != null)
            {
                graph.AddNode(NodeFactory.Build(data), data.NodePosition);
                m_activeSource.AddNode(data);
            }
        }

        private void ShowEmptyText(string message)
        {
            var box = new Box { style = { alignItems = Align.Center } };
            box.StretchToParentSize();
            var label = new Label(message) { style = { top = 50 } };
            box.Add(label);
            rootVisualElement.Add(box);
        }

        void PlayModeStateChanged(UnityEditor.PlayModeStateChange state)
        {
            Init();
        }

        void OnNodeCreationRequest(NodeCreationContext context)
        {
            m_searchWindow.ChangeSearchEntries(m_activeSource.NodeDataCreator.GetSearchTreeEntries());
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), m_searchWindow);
        }

        internal void ChangeDataSource(IEventDataSource source)
        {
            m_activeSource = source;
            RecreateGraph(source);
        }
    }
}
#endif