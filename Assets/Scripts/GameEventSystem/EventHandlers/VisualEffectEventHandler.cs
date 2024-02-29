using System;
using Project.GameDb.ScriptableDatabase;

namespace Project.GameEventSystem
{
    public class VisualEffectEventHandler : EventHandler
    {
        readonly Action<VisualEffectEventData> VisualEffectCallback;
        readonly Lazy<IVFXRepository> _lazyVFXRepo;
        public VisualEffectEventHandler(IEventAPI eventAPI) : base(eventAPI)
        {
            VisualEffectCallback = OnVisualEffectCallback;
        }

        public override void RegisterToAPI()
        {
            m_eventAPI.PlayVFXEvent.Subscribe(VisualEffectCallback);
        }

        public override void UnregisterFromAPI()
        {
            m_eventAPI.PlayVFXEvent.Unsubscribe(VisualEffectCallback);
        }

        void OnVisualEffectCallback(VisualEffectEventData data){
            //TODO implement VIsual effect manager
            UnityEngine.Debug.Log("VFX: " + data.EffectId);

            _lazyVFXRepo.Value?.GetVFX(data.EffectId);
        }
    }
}