using System;
using Project.Utils;

namespace Project.GameFlowSystem
{
    public class DelayedGameStateBuilder : ScriptableGameStateBuilder
    {
        public override IGameState BuildState(SequenceData data, CommandProvider commandProvider){
            // TODO: based on the task command type, try to get command from other system (e.g. Game Command System)
            // then create the DelayedGameState with that command IEnumerator task
            return new FakeDelayedGameState(task: commandProvider.GetCommand(data.commandType).GetTask(), estimatedTime: 10, onProgressCallback: null, onCompleteCallback: null);
        }
        
        public override IGameLink BuildLink(SequenceLinkData link, IGameState nextState, EventProvider eventProvider){
            //TODO: create A link waits for the that task command is finished then go to the next state

            if(link.linkType == SequenceLinkData.LinkType.Event){
                return BuildEventLink(link, nextState, eventProvider);
            }
            else{ // Default
                throw new NotImplementedException($"Link type {link.linkType} not supported or invalid for this state");
            }
        }

        private IGameLink BuildEventLink(SequenceLinkData link, IGameState nextState, EventProvider eventProvider)
        {
            
            ActionWrapper eventWrapper = new ActionWrapper(
                subscribe: (System.Action listener)=>{
                // subscribe to the command complete event
                    eventProvider.RegisterToGameEvent(link.eventType, listener);
                },
                unsubscribe: (System.Action listener)=>{
                // unsubscribe to the command complete event
                    eventProvider.UnregisterFromGameEvent(link.eventType, listener);
                }
            );
            return new EventGameLink(eventWrapper, nextState);
        }
    }
}