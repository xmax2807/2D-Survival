using System;
using System.Collections.Generic;

namespace Project.GameEventSystem.EventGraph{
    public abstract class RuntimeNode
    {
        public readonly Guid Id;
        public RuntimeNode(Guid id) => Id = id;
        public RuntimeNode(string id) => Id = Guid.Parse(id);
        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void Update();
        public abstract void AddChild(RuntimeNode child);
        public abstract void RemoveChild(RuntimeNode child);
        public abstract RuntimeNode next {get;}
    }

    public abstract class SMNode : RuntimeNode{
        public Guid RootId; // determine which state machine this node belongs to
        int closestAvailableSlot; // this < m_children.length
        protected SMNode[] m_children;
        public SMNode this[int i] => m_children[i];
        public SMNode(Guid id, Guid rootId, int childCount) : base(id) {
            if(childCount > 0){
                m_children = new SMNode[childCount];
            }
            RootId = rootId;
        }

        public abstract System.Collections.IEnumerator Run(IStateMachineContext ctx);

        void UpdateAvailableSlot(int startingIndex){
            closestAvailableSlot = startingIndex;

            for(int i = startingIndex; i < m_children.Length; i++){
                if(m_children[i] == null){
                    closestAvailableSlot = i;
                    return;
                }
            }
        }

        public override void AddChild(RuntimeNode child){
            if(child is not SMNode realChild){
                throw new System.InvalidCastException();
            }
            if(closestAvailableSlot < m_children.Length){
                m_children[closestAvailableSlot] = realChild;
                UpdateAvailableSlot(closestAvailableSlot + 1);
                return;
            }
            throw new System.IndexOutOfRangeException();
        }
        public override void RemoveChild(RuntimeNode child){
            if(closestAvailableSlot <= 0){
                return;
            }

            int i = 0;
            for(; i < m_children.Length; i++){
                if(m_children[i] == child){
                    m_children[i] = null;
                    if(i < closestAvailableSlot){
                        closestAvailableSlot = i;
                    }
                    break;
                }
            }
        }
    }
}