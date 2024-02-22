using System;

namespace Project.GameEventSystem
{
    public class PhysicEventHandler : EventHandler
    {
        Action<MaterialDetectionEventData> MaterialDetectionCallback;

        public PhysicEventHandler(IEventAPI eventAPI) : base(eventAPI)
        {
            MaterialDetectionCallback = HandleMaterialDetectionCallback;
        }

        protected override void RegisterToAPI()
        {
            m_eventAPI.MaterialDetectionEvent.Subscribe(MaterialDetectionCallback);
        }

        protected override void UnregisterFromAPI(){
            m_eventAPI.MaterialDetectionEvent.Unsubscribe(MaterialDetectionCallback);
        }

        void HandleMaterialDetectionCallback(MaterialDetectionEventData data){
            //TODO call Physic api to detect the material.
            // int MaterialId = Physic.DetectMaterial(DetectionType type);
            // base on the feedback type from data, call another event

            //swith(data.FeedbackType)
            // sound =>
            // get sound id from Sound database. int sound_id = SoundDb.GetSoundIdFromMaterialId(MaterialId)
            // call m_eventAPI.PlaySoundEvent.Invoke(sound_id);
        }
    }
}