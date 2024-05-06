#if UNITY_EDITOR
using UnityEngine;

namespace Project.GameEventSystem.EventGraph.Editor
{
    [CreateAssetMenu(fileName = "FSMDataSource", menuName = "GameEventSystem/EventGraph/FSMDataSource")]
    public class FSMDataSource : EventDataSource
    {
        [SerializeField] private StateDataSource[] _states;
        public StateDataSource[] States => _states;

        private INodeDataCreator _nodeDataCreator;
        private INodeDataCreator _childNodeDataCreator;

        public override INodeDataCreator NodeDataCreator {
            get{
                if(null == _nodeDataCreator){
                    _nodeDataCreator = new StateMachineNodeCreator(this);
                }
                return _nodeDataCreator;
            }
        }

        public INodeDataCreator ChildNodeDataCreator {
            get{
                if(null == _childNodeDataCreator){
                    _childNodeDataCreator = new StateNodeDataCreator();
                }
                return _childNodeDataCreator;
            }   
        }
    }
}
#endif