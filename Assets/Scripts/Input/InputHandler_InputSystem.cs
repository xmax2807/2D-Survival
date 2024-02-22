
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Project.InputHandler
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputHandler_InputSystem : MonoBehaviour, IInputHandler
    {
        private List<IPlayerControlInputListener> _playerControlInputListeners;

        public void OnMove(InputAction.CallbackContext context)
        {
            foreach(var listener in _playerControlInputListeners){
                listener.OnMove(context.ReadValue<Vector2>(), context.phase);
            }
        }

        public void RegisterPlayerControlListener(IPlayerControlInputListener listener)
        {
            if(listener == null) return;
            _playerControlInputListeners ??= new List<IPlayerControlInputListener>();
            _playerControlInputListeners.Add(listener);
        }

        public void UnregisterPlayerControlListener(IPlayerControlInputListener listener)
        {
            if(listener == null || _playerControlInputListeners == null) return;
            _playerControlInputListeners.Remove(listener);
        }
    }
}