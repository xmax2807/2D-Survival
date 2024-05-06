#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
namespace Project.GameEventSystem.EventGraph.Editor
{
    public class EventGraphConfig
    {
        internal readonly EventNodeBuilderFactory NodeBuilderFactory;

        internal EventGraphConfig(EventNodeBuilderFactory factory)
        {
            NodeBuilderFactory = factory;
        }
    }

    public abstract class ScriptableGraphConfig : ScriptableObject{
        bool isInitialized = false;
        private EventNodeBuilderFactory _nodeBuilderFactory;
        private EventGraphConfig _config;
        public EventGraphConfig GetConfig(){
            if(isInitialized == false){
                _nodeBuilderFactory = new EventNodeBuilderFactory();
                OnInitializeFactory(_nodeBuilderFactory);
                _config = new EventGraphConfig(_nodeBuilderFactory);
                isInitialized = true;
            }
            return _config;
        }

        protected abstract void OnInitializeFactory(EventNodeBuilderFactory builderFactory);

        public void OnDisable(){
            isInitialized = false;
            _config = null;
            _nodeBuilderFactory = null;
        }
    }
}
#endif