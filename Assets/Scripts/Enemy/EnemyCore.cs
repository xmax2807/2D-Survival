using System;
using System.Threading.Tasks;
using Project.CharacterBehaviour;
using UnityEngine;

namespace Project.Enemy
{
    public class EnemyCore : Core
    {
        [SerializeField] private MonoCoreComponent[] m_registeredComponents;

        async void Awake()
        {
            for (int i = 0; i < m_registeredComponents.Length; ++i)
            {
                AddCoreComponent(m_registeredComponents[i]);
                m_registeredComponents[i].SetCore(this);
            }
            var enemyStateNotifier = new EnemyNotifier(this);
            AddCoreComponent<IEnemyStateObservable>(enemyStateNotifier);
            AddCoreComponent<IEnemyStateInvoker>(enemyStateNotifier);

            await Task.CompletedTask;
        }
    }
}