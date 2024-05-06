using System;
using System.Collections.Generic;
using UnityEngine;
namespace Project.GameEventSystem.EventGraph{
    public abstract class EventNodeItemData : ScriptableObject
    {
    }
    public abstract class EventNodeData : ScriptableObject, ISerializationCallbackReceiver, IEquatable<EventNodeData>{
        public Guid Id;
        [SerializeField] private string idString;

        public bool Equals(EventNodeData other) => Id == other.Id;

        public virtual void OnAfterDeserialize(){
            Guid.TryParse(idString, out Id);
        }

        public virtual void OnBeforeSerialize() {
            idString = Id.ToString();
        }

        #region Editor parts
        #if UNITY_EDITOR
        [HideInInspector]public Vector2 NodePosition;
        [HideInInspector]public bool IsActive;
        #endif
        #endregion
    }

    public abstract class EventNodeData<TItem> : EventNodeData
    {
        [SerializeField] protected TItem[] m_items;

        #if UNITY_EDITOR
        public event Action OnItemsChanged;
        protected void InvokeItemsChanged() => OnItemsChanged?.Invoke();
        #endif
    }
}