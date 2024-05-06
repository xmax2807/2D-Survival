using System.Collections;

namespace Project.GameEventSystem.EventGraph{
    public class VoidCommand : IFSMCommand
    {
        int m_commandId;
        System.Func<IStateMachineContext, int[]> m_paramGetter;
        public VoidCommand(int commandId, System.Func<IStateMachineContext, int[]> paramGetter){
            m_commandId = commandId;
            m_paramGetter = paramGetter;
        }
        public IEnumerator Execute(IStateMachineContext context)
        {
            ICommandProvider provider = context.GetProvider<ICommandProvider>();
            if(provider != null){
                provider.ExecuteCommand(m_commandId, m_paramGetter.Invoke(context));
            }
            else{
                yield return null;
            }
        }
    }

    /// <summary>
    /// Command only execute when condition is true
    /// </summary>
    public class ConditionalCommand : IFSMCommand
    {
        readonly IFSMCommand m_command;
        readonly ICommandCondition m_condition;

        //TODO add operator to compare the result

        public ConditionalCommand(IFSMCommand command, ICommandCondition condition)
        {
            m_command = command;
            m_condition = condition;
        }

        public IEnumerator Execute(IStateMachineContext context)
        {
            yield return m_condition.Execute(context);
            if(m_condition.IsConditionMet()){
                yield return m_command.Execute(context);
            }
        }
    }
}