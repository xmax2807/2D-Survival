using System;
using Project.SaveSystem;
using UnityEngine;

namespace Project.PlayerBehaviour
{
    [CreateAssetMenu(fileName = "PlayerManager", menuName = "Player/PlayerManager")]
    public class PlayerManager : ScriptableObject, IBindRegister
    {
        [SerializeField] private PlayerCore playerTemplate;
        private readonly PlayerDataBinding playerDataBinding = new();

        void OnEnable(){
            ScriptableBindRegistry.OnBindRegistryCreated += OnBindRegistryCreated;
        }
        void OnDisable(){
            ScriptableBindRegistry.OnBindRegistryCreated -= OnBindRegistryCreated;
        }

        public void OnBindRegistryCreated(IBindRegistry registry)
        {
            registry.Register<PlayerData>(playerDataBinding);
        }


        public void OnBindRegistryDestroyed(IBindRegistry registry)
        {
            registry.Unregister<PlayerData>(playerDataBinding);
        }
    }
}