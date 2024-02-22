using System;
using Project.Utils;

namespace Project.GameFlowSystem
{
    /// <summary>
    /// Link that straightly go to the next state
    /// A state should only has one default link
    /// </summary>
    public class GameLink : DefaultGameLink
    {
        public GameLink(IGameState nextState) : base(nextState){}
        protected override bool InnerValidation () => true;
    }


    /// <summary>
    /// Typically, use this when a button was pressed.
    /// </summary>
    public class EventGameLink : DefaultGameLink
    {
        bool isRaisedEvent;

        // easily subscribe and unsubscribe to external event without knowledge of the event type
        readonly ActionWrapper _executeCallback;
        public EventGameLink(ActionWrapper executeCallback, IGameState nextState) : base (nextState){
            _executeCallback = executeCallback;
        }
        public override void Disable()
        {
            // unsubscribe to the event that wrapped in the ActionWrapper
            _executeCallback.RemoveSubscriber(MarkAsRaised);
            // reset the flag
            isRaisedEvent = false;
        }

        public override void Enable()
        {
            // subscribe to the event that wrapped in the ActionWrapper
            _executeCallback.AddSubscriber(MarkAsRaised);
            // reset the flag
            isRaisedEvent = false;
        }

        private void MarkAsRaised()
        {
            isRaisedEvent = true;
        }

        
        protected override bool InnerValidation() => isRaisedEvent;
    }
}