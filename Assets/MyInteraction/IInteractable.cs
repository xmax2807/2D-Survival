namespace MyInteraction{

    public interface IInteractorFeedback{
        void HideFeedback();
        void ShowIndicator(int iconId);
        void ShowPrompt(string promptText);
    }
    public interface IInteractor{
        event System.Action<int> InteractorKeyPressEvent;
        UnityEngine.Vector3 GetInteractionPosition();
        IInteractorFeedback GetInteractorFeedback();
    }
    public interface IInteractable{

        UnityEngine.Vector3 GetPosition();

        void Interact(IInteractor interactor);
        void UpdateLogic(InteractionContext context); // call every frame
        void UpdatePhysics(InteractionContext context); // call every fixed update
    }
}