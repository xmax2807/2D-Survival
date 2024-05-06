using System;
using System.Collections;

namespace Project.GameEventSystem.EventGraph
{
    public class StateCommandNode : SMNode
    {
        private SMNode nextNode;
        public override RuntimeNode next => nextNode;
        public StateCommandNode(Guid id, Guid rootId, SMNode nextNode = null) : base(id, rootId, 0)
        {
            this.nextNode = nextNode;
        }
        public override void OnEnter(){}

        public override void OnExit(){}

        public override IEnumerator Run(IStateMachineContext ctx)
        {
            //TODO run list of commands
            //yield return null for now
            yield return null;
        }

        public override void Update(){}
        public override void AddChild(RuntimeNode child)
        {
            nextNode = (SMNode)child;
        }
        public override void RemoveChild(RuntimeNode child)
        {
            nextNode = null;
        }


        public void AddCommand(IFSMCommand[] commands)
        {
            
        }
    }
}