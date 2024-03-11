using System.Collections.Generic;
using UnityEngine;
namespace Project.GameFlowSystem
{
    public abstract class AbstractSequenceBuildDirector : ScriptableObject, ISequenceBuildDirector
    {
        [SerializeField] protected CommandProvider CommandProvider;
        [SerializeField] protected EventProvider EventProvider;
        public IGameState DefaultState { get; protected set; } // init state
        protected Dictionary<int, IGameState> m_cache = new();

        public abstract void BuildSequences(ref List<IGameState> gameStates, IGameStateFactory factory, ScriptableSequenceData data);
        protected IGameState RecursivelyCreateState(IGameStateFactory factory, ScriptableSequenceData data)
        {

            //Create single state
            IGameState gameState;
            if (m_cache.ContainsKey(data.Id))
            {
                gameState = m_cache[data.Id];
            }
            else
            {
                gameState = factory.Resolve(data.BuilderId).BuildState(data.SequenceData, CommandProvider);
                m_cache.Add(data.Id, gameState);
            }

            // if there is no link, return
            if (data.Links == null || data.Links.Length == 0)
            {
                return gameState;
            }

            //Create links
            for (int i = data.Links.Length - 1; i >= 0; --i)
            {
                var sequenceData = data.Links[i].SequenceData;
                if(sequenceData == null) { continue; }
                
                IGameState linkState;

                if (m_cache.ContainsKey(sequenceData.Id))
                {
                    linkState = m_cache[sequenceData.Id];
                }
                else
                {
                    linkState = RecursivelyCreateState(factory, sequenceData);
                }

                IGameLink link = BuildLink(data.Links[i].Data, linkState, EventProvider);

                gameState.AddLink(link);
            }

            return gameState;
        }

        protected IGameLink BuildLink(SequenceLinkData data, IGameState nextState, EventProvider eventProvider)
        {
            switch (data.linkType)
            {
                case SequenceLinkData.LinkType.Event:
                    {
                        ActionWrapper eventWrapper = new(
                            subscribe: (System.Action listener) =>
                            {
                                // subscribe to the command complete event
                                eventProvider.RegisterToGameEvent(data.eventType, listener);
                            },
                            unsubscribe: (System.Action listener) =>
                            {
                                // unsubscribe to the command complete event
                                eventProvider.UnregisterFromGameEvent(data.eventType, listener);
                            }
                        );

                        return new EventGameLink(eventWrapper, nextState);
                    }
                default: return new GameLink(nextState);
            }
        }

        void OnDisable()
        {
            m_cache?.Clear();
        }
        void OnEnable()
        {
            m_cache ??= new Dictionary<int, IGameState>();
        }
    }
}