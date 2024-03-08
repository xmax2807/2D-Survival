using System;
using Project.GameDb;
using Project.GameDb.ScriptableDatabase;
using Project.Manager;

namespace Project.GameEventSystem
{
    public class VisualEffectEventHandler : EventHandler
    {
        readonly Action<VisualEffectEventData> VisualEffectCallback;
        readonly Lazy<IVFXRepository> _lazyVFXRepo;
        public VisualEffectEventHandler(IEventAPI eventAPI) : base(eventAPI)
        {
            _lazyVFXRepo = new Lazy<IVFXRepository>(() => GameManager.RepoProvider.GetRepository<IVFXRepository>());
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
                    GameManager.VFXManager.AnimatorEffectService.PlayEffectAt(effect.stateId, data.Position);
                }
            }
        }
    }
}