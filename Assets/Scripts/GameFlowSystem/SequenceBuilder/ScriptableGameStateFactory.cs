using System.Collections.Generic;
using UnityEngine;

namespace Project.GameFlowSystem
{
    [CreateAssetMenu(fileName = "GameStateFactory", menuName = "FlowSystem/GameStateFactory")]
    public class ScriptableGameStateFactory : AbstractGameStateFactory
    {
        
        [SerializeField] ScriptableGameStateBuilder[] AvailableBuilders;

        protected override void InitBuilders()
        {
            cacheBuilders = new Dictionary<string, IGameStateBuilder>();

            foreach (var builder in AvailableBuilders){
                cacheBuilders.Add(builder.IdentificationName, builder);
            }
        }
    }
}