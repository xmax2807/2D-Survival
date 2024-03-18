using System.Collections;
using Project.GameEvent;
using Project.GameEventSystem;
using UnityEngine;

namespace Project.Manager
{
    public class GameEventSystemInitializer : MonoSystemInitializer
    {
        [SerializeField] string m_gameEventProviderPath = "EventSystem_EventProvider";
        [SerializeField] string m_gameEventAPIPath = "EventSystem_GameEventAPI";
        
        private GameEventService _gameEventService;
        private ScriptableEventProvider _gameEventProvider;
        private GameEventAPI _gameEventAPI;
        
        protected override IEnumerator InitializeInternal(GameManager manager)
        {
            var request =  Resources.LoadAsync<GameEventSystem.ScriptableEventProvider>(m_gameEventProviderPath);
            yield return request;
            _gameEventProvider = request.asset as GameEventSystem.ScriptableEventProvider;
            
            request = Resources.LoadAsync<GameEventAPI>(m_gameEventAPIPath);
            yield return request;
            _gameEventAPI = request.asset as GameEventAPI;
            
            DefineEventHandlers();

            manager.AddService<IEventProvider>(_gameEventProvider);
            manager.AddService<IEventAPI>(_gameEventAPI);
            manager.AddService<IGameEventRegister>(_gameEventService);
            manager.AddService<IGameEventPublisher>(_gameEventService);
        }

        void DefineEventHandlers()
        {
            _gameEventService = new GameEventService();
            _gameEventService.AddHandler(new SoundEventHandler(_gameEventAPI));
            _gameEventService.AddHandler(new VisualEffectEventHandler(_gameEventAPI));
            _gameEventService.AddHandler(new PhysicEventHandler(_gameEventAPI));
            _gameEventService.AddHandler(new ItemEventHandler(_gameEventAPI));
            _gameEventService.AddHandler(new ItemDropEventHandler(_gameEventAPI));
        }
    }
}