using System.Collections.Generic;
using UnityEngine;
namespace Project.AnimationEventSystem
{
    [CreateAssetMenu(fileName = "AnimationEventDb", menuName = "AnimationEventSystem/AnimationEventDb")]
    public class AnimationEventDb : ScriptableObject
    {
        [SerializeField] int id_playSoundEvent;
        [SerializeField] int id_playParticleEvent;
        [SerializeField] int id_materialDetectionEvent;

        Dictionary<int, AnimationDataPool> m_eventDataMap;
        AnimationDataPool m_poolForEmpty;
        void OnEnable(){
            Init();
        }
        void OnDisable(){
            m_eventDataMap.Clear();
        }
        void Init(){
            m_poolForEmpty = new AnimationDataPool(AnimationEventData.Empty);
            m_eventDataMap = new Dictionary<int, AnimationDataPool>
            {
                { id_playSoundEvent, new AnimationDataPool(new SoundAnimationEventData()) },
                // m_eventDataMap.Add(id_playParticleEvent, new AnimationDataPool(new ParticleAnimationEventData()));
                { id_materialDetectionEvent, new AnimationDataPool(new MaterialDetectionEventData()) }
            };
        }

        public AnimationEventData GetDataFromPool(int id){
            bool canGetData = m_eventDataMap.ContainsKey(id);
            if(!canGetData){
                return m_poolForEmpty.GetData();
            }
            AnimationEventData result = m_eventDataMap[id].GetData();
            return result;
        }

        public void ReturnDataToPool(int id, AnimationEventData data){
            bool canGetData = m_eventDataMap.ContainsKey(id);
            if(!canGetData){
                m_poolForEmpty.ReturnData(data as EmptyAnimationEventData);
                return;
            }
            m_eventDataMap[id].ReturnData(data);
        }


        #region Static
        #if UNITY_EDITOR

        void OnValidate(){
            m_ids = GetIds();
        }
        private static int[] m_ids;
        public int[] GetIds()
        {
            m_ids = new int[]{
                id_playSoundEvent,
                id_playParticleEvent,
                id_materialDetectionEvent
            };
            return m_ids;
        }

        public static int GetIdAt(int index) => m_ids[index];
        public static int FindIndex(int id) => System.Array.IndexOf(m_ids, id);

        private static string[] m_idStrings;
        public static string[] GetMapIdStrings(){
            m_idStrings ??= new string[]{
                    $"PlaySoundEvent: {m_ids[0]}",
                    $"PlayParticleEvent: {m_ids[1]}",
                    $"MaterialDetectionEvent: {m_ids[2]}",
                };

            return m_idStrings;
        }
        #endif
        #endregion Static
    }
}