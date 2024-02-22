using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Project.SpawnSystem
{
    [CreateAssetMenu(fileName = "RewardRegistry", menuName = "SpawnSystem/RewardRegistry")]
    public class ScriptableRewardableEntityRegistry : ScriptableObject, IRewarableEntityRegistry
    {
        private Dictionary<int, IRewardableEntity> m_registeredEntities = new Dictionary<int, IRewardableEntity>();

        public IRewardableEntity GetById(int targetId) {
            if(m_registeredEntities.ContainsKey(targetId) == false) {
                Debug.LogError("Can't find entity with id: " + targetId);
                return null;
            }
            return m_registeredEntities[targetId];
        }
        public IRewardableEntity First() => m_registeredEntities.Values.First();
        public void Register(int id, IRewardableEntity entity){
            if(entity == null || m_registeredEntities.ContainsKey(id)) {
                return;
            }
            Debug.Log("Registering entity: " + entity + " with id: " + id);
            m_registeredEntities[id] = entity;
        }
        public void Unregister(int id){
            if(m_registeredEntities.ContainsKey(id)) {
                m_registeredEntities.Remove(id);
            }
        }
    }
}