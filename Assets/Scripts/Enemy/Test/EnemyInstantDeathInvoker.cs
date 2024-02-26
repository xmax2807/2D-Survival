using System.Collections;
using Project.SpawnSystem;
using UnityEngine;

namespace Project.Enemy
{
    public class EnemyInstantDeathInvoker : MonoBehaviour
    {
        [SerializeField] private Enemy.EnemyCore core;
        [SerializeField] private ScriptableRewardableEntityRegistry rewardableEntityRegistry;

        private IEnemyStateInvoker m_notifier;

        private void Notify(){
            if(m_notifier == null){
                m_notifier = core.GetCoreComponent<IEnemyStateInvoker>();
            }
            m_notifier.NotifyDeathEvent(new EnemyDeathData(id: core.GetId(), killerId: rewardableEntityRegistry.First().Id, 0, 0));
        }

        void Start(){
            StartCoroutine(Run());
        }

        IEnumerator Run(){
            yield return new WaitForEndOfFrame();
            Notify();
        }
    }
}