using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.GameEventSystem.EventGraph
{
    /// <summary>
    /// a class controls a state machine 
    /// </summary>
    public class StateMachineRunner {
        StateMachineTree m_target;
        public Coroutine Coroutine {get;private set;}
        public int[] Parameters {get;private set;}
        public StateMachineResult Result {get;private set;}
        public bool IsEmpty => m_target == null;
        public bool IsDone => Result.Status == StateMachineStatus.Done;
        public System.Guid Id => m_target.Id;

        public void MarkAsDone(int value){
            Result = new StateMachineResult(StateMachineStatus.Done, value);
        }

        private StateMachineRunner(StateMachineTree tree, int[] parameters){
            this.m_target = tree;
            this.Parameters = parameters;
        }
        private static Stack<StateMachineRunner> m_pool = new Stack<StateMachineRunner>(3);
        public static StateMachineRunner GetRunner(StateMachineTree target, int[] parameters){
            StateMachineRunner result;
            if (m_pool.Count > 0){
                result = m_pool.Pop();
                result.Init(target, parameters);
            }
            else{
                result = new StateMachineRunner(target, parameters);
            }
            return result;
        }

        public static void ReturnRunner(StateMachineRunner runner){
            if(runner == null){
                return;
            }
            runner.CleanUp();
            m_pool.Push(runner);
        }

        private static StateMachineRunner _empty;
        public static StateMachineRunner Empty => _empty ??= new StateMachineRunner(null, null);

        private void Init(StateMachineTree target, int[] parameters)
        {
            m_target = target;
            Parameters = parameters;
            Coroutine = null;
            Result = StateMachineResult.Default;
        }

        private void CleanUp(){
            m_target = null;
            Parameters = null;
            Coroutine = null;
            Result = StateMachineResult.Default;
        }
    }
    public interface IStateMachineContext{
        StateMachineRunner RequestRunStateMachine(System.Guid targetId, int[] parameters);
        /// <summary>
        /// Completely remove this machine
        /// </summary>
        /// <param name="runner"></param>
        void ReturnRunner(StateMachineRunner runner);
        /// <summary>
        /// Mark this machine as done with result => not yet removed in the context
        /// </summary>
        /// <param name="machineId"></param>
        /// <param name="result"></param>
        void FinishMachineWith(System.Guid machineId, int result);

        TProvider GetProvider<TProvider>();
        int GetParamValue(Guid machineId, int index);
    }
    public class StateMachineContext : IStateMachineContext
    {
        readonly Dictionary<System.Guid, StateMachineRunner> m_activeRunningMachines;
        readonly StateMachineTree[] m_stateMachineTrees;

        public StateMachineContext(StateMachineTree[] stateMachineTrees){
            m_stateMachineTrees = stateMachineTrees;
            m_activeRunningMachines = new Dictionary<System.Guid, StateMachineRunner>();
        }
        public StateMachineRunner RequestRunStateMachine(Guid targetId, int[] parameters)
        {
            if(m_activeRunningMachines.ContainsKey(targetId)){
                // can not run the same state machine twice
                return StateMachineRunner.Empty;
            }

            StateMachineTree stateMachineTree = FindStateMachineTree(targetId);
            if(stateMachineTree == null){
                return StateMachineRunner.Empty;
            }
            var runner = StateMachineRunner.GetRunner(stateMachineTree, parameters);
            m_activeRunningMachines.Add(targetId, runner);
            return runner;
        }

        public void ReturnRunner(StateMachineRunner runner)
        {
            if(runner == null || !m_activeRunningMachines.ContainsKey(runner.Id)){
                return;
            }
            m_activeRunningMachines.Remove(runner.Id);
            StateMachineRunner.ReturnRunner(runner);
        }

        public void FinishMachineWith(System.Guid machineId, int result){
            if(m_activeRunningMachines.ContainsKey(machineId)){
                m_activeRunningMachines[machineId].MarkAsDone(result);
            }
        }

        private StateMachineTree FindStateMachineTree(System.Guid targetId)
        {
            for(int i = 0; i < m_stateMachineTrees.Length; i++){
                if(m_stateMachineTrees[i].Id == targetId){
                    return m_stateMachineTrees[i];
                }
            }
            return null;
        }

        public TProvider GetProvider<TProvider>()
        {
            throw new NotImplementedException();
        }

        public int GetParamValue(Guid machineId, int index)
        {
            m_activeRunningMachines.TryGetValue(machineId, out var runner);
            if(runner == null){
                return -1;
            }
            return runner.Parameters[index];
        }
    }
}