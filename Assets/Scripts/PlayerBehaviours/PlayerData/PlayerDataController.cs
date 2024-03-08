using System;
using System.Collections;
using Project.CharacterBehaviour;
using Project.GameDb;
using UnityEngine;
namespace Project.PlayerBehaviour
{
    public class PlayerDataController : MonoCoreComponent
    {
        [SerializeField] ScriptPlayerData _playerData;

        protected override void AfterAwake()
        {
            base.AfterAwake();
            AddCoreComponent<PlayerDataController>(this);
        }

        internal void ReceiveGold(int amount)
        {
            _playerData.Gold += amount;
        }
    }
}