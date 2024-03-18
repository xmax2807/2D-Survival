using System;
using Project.GameDb;
using Project.SaveSystem;
using UnityEngine;

namespace Project.PlayerBehaviour
{
    [CreateAssetMenu(fileName = "PlayerManager", menuName = "Player/PlayerManager")]
    public class PlayerManager : ScriptableObject, IBindRegister
    {
        [SerializeField] private PlayerCore playerTemplate;
        public PlayerCore TemplatePrefab => playerTemplate;

        [SerializeField] private ScriptPlayerData scriptPlayerData;
        public ScriptPlayerData ScriptPlayerData => scriptPlayerData;
        
        private readonly PlayerDataBinding playerDataBinding = new();
        public PlayerData PlayerData => playerDataBinding.PlayerData;

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