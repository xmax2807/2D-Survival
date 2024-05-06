using System.Collections;
using System.Collections.Generic;

namespace Project.GameEventSystem.EventGraph
{
    public interface ICommandCondition : IFSMCommand, ICondition {}
    public class SingleCommandCondition : ICommandCondition
    {
        private int m_commandId;
        private System.Func<IStateMachineContext, int[]> m_paramGetter;
        private int expectedResult;
        private bool m_isMet = false;

        public SingleCommandCondition(int commandId, System.Func<IStateMachineContext, int[]> paramGetter, int expectedResult){
            m_commandId = commandId;
            m_paramGetter = paramGetter;
            this.expectedResult = expectedResult;
        }
        public IEnumerator Execute(IStateMachineContext context)
        {
            ICommandProvider provider = context.GetProvider<ICommandProvider>();
            if(provider != null){
                int result = provider.ExecuteCommandWithResult(m_commandId, m_paramGetter.Invoke(context));
                m_isMet = (result == expectedResult);
            }
            else{
                yield return null;
            }
        }

        public bool IsConditionMet() => m_isMet;
    }


    /// <summary>
    /// these conditions must all be met (and operator)
    /// </summary>
    public class CompoundCommandCondition : ICommandCondition
    {
        private bool m_isMet = false;
        private readonly ICommandCondition[] m_conditions;

        public CompoundCommandCondition(params ICommandCondition[] conditions){
            m_conditions = conditions;
        }

        public IEnumerator Execute(IStateMachineContext context)
        {
            m_isMet = true;
            for(int i = 0; i < m_conditions.Length; i++){
                yield return m_conditions[i].Execute(context);
                m_isMet = m_isMet && m_conditions[i].IsConditionMet();
                
                if(!m_isMet) break;
            }
        }

        public bool IsConditionMet() => m_isMet;
    }
    
}