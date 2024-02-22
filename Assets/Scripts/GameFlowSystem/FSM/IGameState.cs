using System.Collections;
using System.Collections.Generic;

namespace Project.GameFlowSystem
{
    public interface IGameState
    {
        void Enter();
        IEnumerator Execute(); //Update every frame
        void Exit();
        void AddLink(IGameLink link);
        void RemoveLink(IGameLink link);
        void EnableAllLinks();
        void DisableAllLinks();
        bool TryGetNextState(out IGameState nextState);
    }
}