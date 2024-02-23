using System.Collections;
using UnityEngine;

namespace Project.Enemy
{
    public class EnemyInstantDeathInvoker : MonoBehaviour
    {
        [SerializeField] private Enemy.EnemyCore core;
        [SerializeField] private int playerId;

        private IEnemyStateInvoker m_notifier;

        private void Notify(){
            if(m_notifier == null){
                m_notifier = core.GetCoreComponent<IEnemyStateInvoker>();
            }
            m_notifier.NotifyDeathEvent(new EnemyDeathData(id: core.GetId(), killerId: playerId, 0, 0));
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