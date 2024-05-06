using System.Collections.Generic;

namespace Project.GameEventSystem.EventCommand
{
    public interface IEventCommandProvider{
        #if UNITY_EDITOR
        int GetCommandId(string commandName);
        string GetCommandName(int commandId);
        string[] GetResultCommandNames();
        string[] GetVoidCommandNames();
        #endif
    }
}