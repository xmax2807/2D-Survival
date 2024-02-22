using System;
using System.Collections.Generic;

namespace Project.GameEvent
{
    public enum GameEventType{
        GetEffect,
        EffectAdded,
        EffectRemoved,
    }

    public interface IGameEventRegister{
        void Register<TData>(GameEventType type, Action<TData> callback);
        void Unregister<TData>(GameEventType type, Action<TData> callback);
    }
    public interface IGameEventPublisher{
        void Publish<TData>(GameEventType type, TData data);
    }
    public class GameEventService : IGameEventRegister, IGameEventPublisher
    {
        private readonly Dictionary<GameEventType, List<Delegate>> _eventMap;
        private readonly List<GameEventSystem.EventHandler> _eventHandlers;
        public GameEventService(){
            _eventHandlers = new List<GameEventSystem.EventHandler>();
            _eventMap = new Dictionary<GameEventType, List<Delegate>>();
        }
        public void Unregister<TData>(GameEventType type, Action<TData> callback)
        {
            if(_eventMap.ContainsKey(type)){
                _eventMap[type].Remove(callback);
            }
        }

        public void Register<TData>(GameEventType type, Action<TData> callback){
            if(!_eventMap.ContainsKey(type)){
                _eventMap.Add(type, new List<Delegate>());
            }
            _eventMap[type].Add(callback);
        }

        public void Publish<TData>(GameEventType type, TData data){
            if(_eventMap.ContainsKey(type)){
                foreach(var callback in _eventMap[type]){
                    (callback as Action<TData>)?.Invoke(data);
                }
            }
        }

        public void AddHandler(Project.GameEventSystem.EventHandler handler){
            if(!_eventHandlers.Contains(handler)){
                handler.RegisterToAPI();
                _eventHandlers.Add(handler);
            }
        }
        public void RemoveHandler(Project.GameEventSystem.EventHandler handler){
            if(_eventHandlers.Contains(handler)){
                handler.UnregisterFromAPI();
                _eventHandlers.Remove(handler);
            }
        }
    }
}