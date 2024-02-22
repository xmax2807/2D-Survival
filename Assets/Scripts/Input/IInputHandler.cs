using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.InputHandler
{
    public interface IInputHandler
    {
        void RegisterPlayerControlListener(IPlayerControlInputListener listener);
        void UnregisterPlayerControlListener(IPlayerControlInputListener listener);
    }

    public interface IPlayerControlInputListener{
        void OnMove(Vector2 direction, InputActionPhase phase);
    }
}