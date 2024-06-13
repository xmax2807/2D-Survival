namespace MyInventory.Testing{
    public class ExamplePlayerInput : UnityEngine.MonoBehaviour{
        public bool IsOpeningInventory;
        public void OnEscapePressed(UnityEngine.InputSystem.InputAction.CallbackContext context){
            if(context.performed){
                IsOpeningInventory = !IsOpeningInventory;
                ExampleEvents.ActiveInventoryEvent.Invoke(IsOpeningInventory);
            }
        }
    }
}