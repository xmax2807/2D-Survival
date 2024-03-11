using System.Collections.Generic;
using UnityEngine;

namespace Project.GameFlowSystem.InProject
{
    [CreateAssetMenu(fileName = "InProject_GameStateFactory", menuName = "GameFlowSystem/InProject/GameStateFactory")]
    public class DefaultGameStateFactory : AbstractGameStateFactory
    {
        protected override void InitBuilders(){
            cacheBuilders = new Dictionary<string, IGameStateBuilder>
            {
                { "LaunchState", new LaunchGameStateBuilder("LaunchState") }
            };
        }
    }
}