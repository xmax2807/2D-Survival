using System;
using System.Collections.Generic;
using UnityEngine;
namespace MyInventory.Testing{
    public class ExampleInventoryInitializer : MonoBehaviour{
        [SerializeField] ScriptableStringStorage itemNameStorage;
        [SerializeField] ItemParamStorage itemParamStorage;
        [SerializeField] ScriptableStringStorage itemDescriptionStorage;
        [SerializeField] ItemIconStorage itemIconStorage;
        [SerializeField] PlayerInventory playerStorage;
        [SerializeField] InventoryUI inventoryUI;

        private ExampleRepository _repository;
        private EventMapper _eventMapper;
        void Awake(){
            _repository = new ExampleRepository(itemNameStorage, itemDescriptionStorage, itemParamStorage, itemIconStorage, playerStorage);
            _eventMapper = new EventMapper();
            var builder = InventoryInitializer.CreateBuilder();
            var initializer = builder.WithEventMapper(_eventMapper)
                                    .WithRepository(_repository)
                                    .WithUI(new InventoryUIController(inventoryUI))
                                    .Build();
            initializer.Intialize();
        }

        System.Collections.IEnumerator Start(){
            yield return new WaitForSeconds(2f);
            _eventMapper.InvokeEvent(InventoryUIActiveEventData.GetFromPool(true, 0f));
        }

        private class EventMapper : IInventoryEventMapper
        {
            private readonly Dictionary<Type, Delegate> _eventStorage = new();
            private LinkedList<IInventoryEventHandler> _handlers;
            public void AttachHandler(IInventoryEventHandler eventHandler)
            {
                _handlers ??= new LinkedList<IInventoryEventHandler>();
                if(_handlers.Contains(eventHandler)) return;

                eventHandler.OnAttachedToMapper(this);
                _handlers.AddLast(eventHandler);
            }

            public void DetachHandler(IInventoryEventHandler eventHandler)
            {
                eventHandler.OnDetachedFromMapper(this);
                _handlers.Remove(eventHandler);
            }

            public void Dispose()
            {
                foreach(var handler in _handlers){
                    handler?.OnDetachedFromMapper(this);
                }
                _handlers.Clear();
            }

            public void SubscribeToEvent<TEvent>(Action<TEvent> callback) where TEvent : BaseInventoryEventData<TEvent>, new()
            {
                Type type = typeof(TEvent);
                if(_eventStorage.TryGetValue(type, out Delegate d)){
                    _eventStorage[type] = Delegate.Combine(d, callback);
                }
                else{
                    _eventStorage.Add(type, callback);
                }
            }

            public void UnsubscribeFromEvent<TEvent>(Action<TEvent> callback) where TEvent : BaseInventoryEventData<TEvent>, new()
            {
                Type type = typeof(TEvent);
                if(_eventStorage.TryGetValue(type, out Delegate d)){
                    _eventStorage[type] = Delegate.Remove(d, callback);
                }
                if(_eventStorage[type] == null){
                    _eventStorage.Remove(type);
                }
            }

            public void InvokeEvent<TEvent>(TEvent eventData) where TEvent : BaseInventoryEventData<TEvent>, new()
            {
                if(_eventStorage.TryGetValue(typeof(TEvent), out Delegate d)){
                    ((Action<TEvent>)d).Invoke(eventData);
                }
            }
        }
    }
}