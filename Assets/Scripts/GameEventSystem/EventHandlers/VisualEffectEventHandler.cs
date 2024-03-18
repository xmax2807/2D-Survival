using System;
using Project.GameDb;
using Project.GameDb.ScriptableDatabase;
using Project.Manager;
using Project.VisualEffectSystem;

namespace Project.GameEventSystem
{
    public class VisualEffectEventHandler : EventHandler
    {
        readonly Action<VisualEffectEventData> VisualEffectCallback;
        readonly Lazy<IVFXRepository> _lazyVFXRepo;
        readonly Lazy<VisualEffectManager> _lazyVFXManager;
        public VisualEffectEventHandler(IEventAPI eventAPI) : base(eventAPI)
        {
            _lazyVFXRepo = new Lazy<IVFXRepository>(() => GameManager.Instance.GetService<IDatabaseRepoProvider>().GetRepository<IVFXRepository>());
            _lazyVFXManager = new Lazy<VisualEffectManager>(() => GameManager.Instance.GetService<VisualEffectManager>());
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

        void OnVisualEffectCallback(VisualEffectEventData data)
        {
            if (data.FXType == VisualEffectEventData.EffectType.Animator)
            {
                AnimatorEffectData effect = _lazyVFXRepo.Value?.GetAnimatorEffect(data.EffectId);

                if (effect != null)
                {
                    _lazyVFXManager.Value?.AnimatorEffectService.PlayEffectAt(effect.stateId, data.Position);
                }
            }
        }
    }
}