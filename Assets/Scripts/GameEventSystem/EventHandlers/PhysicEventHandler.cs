using System;
using System.Collections;
using Project.GameParams;
using Project.Manager;
using UnityEngine;

namespace Project.GameEventSystem
{
    public class PhysicEventHandler : EventHandler
    {
        readonly Action<MaterialDetectionEventData> MaterialDetectionCallback;

        public PhysicEventHandler(IEventAPI eventAPI) : base(eventAPI)
        {
            MaterialDetectionCallback=HandleMaterialDetectionCallback;
        }

        public override void RegisterToAPI()
        {
            m_eventAPI.MaterialDetectionEvent.Subscribe(MaterialDetectionCallback);
        }

        public override void UnregisterFromAPI(){
            m_eventAPI.MaterialDetectionEvent.Unsubscribe(MaterialDetectionCallback);
        }

        void HandleMaterialDetectionCallback(MaterialDetectionEventData data){
            GameManager.Instance.CoroutineCommandQueue.Enqueue(RequestMaterialDetection(data.DectectionType, data.AtPosition));
        }

        IEnumerator RequestMaterialDetection(Enums.MaterialDectectionType type, UnityEngine.Vector2 atPosition){
            yield return new WaitForFixedUpdate();
            //TODO call Physic api to detect the material.
            Enums.MaterialType materialType = Enums.MaterialType.Grass;
            IMaterialParamAPI materialAPI = GameManager.Params.GetParamAPI<IMaterialParamAPI>();
            int sound_id = materialAPI.GetSoundId(materialType);

            m_eventAPI.PlaySoundEvent.Invoke(sound_id);
        } 
    }
}