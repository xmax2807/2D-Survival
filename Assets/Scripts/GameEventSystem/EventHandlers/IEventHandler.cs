using System;

namespace Project.GameEventSystem
{
    public abstract class EventHandler : IDisposable{
        protected readonly IEventAPI m_eventAPI;

        public EventHandler(IEventAPI eventAPI){
            m_eventAPI = eventAPI;
            RegisterToAPI();
        }

        protected virtual void RegisterToAPI(){}
        protected virtual void UnregisterFromAPI(){}

        public void Dispose()
        {
            UnregisterFromAPI();
        }
    }
}