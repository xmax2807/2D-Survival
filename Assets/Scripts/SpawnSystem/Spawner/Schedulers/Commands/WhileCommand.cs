using System;
using System.Collections;

namespace Project.SpawnSystem
{
    public abstract class WhileCommand : ISpawnSchedulerCommand
    {
        private readonly ISpawnSchedulerCommand[] commands;


        public WhileCommand(params ISpawnSchedulerCommand[] commands)
        {
            this.commands = commands;
        }

        public static WhileCommand Create(Func<bool> condition, params ISpawnSchedulerCommand[] commands)
        {
            return new WhileCallbackCommand(condition, commands);
        }
        public static WhileCommand Create(ISpawnValidation validation, params ISpawnSchedulerCommand[] commands)
        {
            return new WhileValidationCommand(validation, commands);
        }

        public IEnumerator Execute(ISpawnSchedulerController scheduler)
        {
            while (CheckCondition(scheduler) == true)
            {
                foreach (var command in commands)
                {
                    yield return command.Execute(scheduler);
                }
            }
            //For the case of the condition is false
            yield return null;
        }

        protected abstract bool CheckCondition(ISpawnSchedulerController scheduler);
    }

    public class WhileCallbackCommand : WhileCommand
    {
        protected Func<bool> condition;
        public WhileCallbackCommand(Func<bool> condition, params ISpawnSchedulerCommand[] commands) : base(commands)
        {
            this.condition = condition;
        }
        protected override bool CheckCondition(ISpawnSchedulerController scheduler)
        {
            return condition != null && condition.Invoke();
        }
    }

    /// <summary>
    /// A command to execute while condition is true where condition is Validation class 
    /// </summary>
    public class WhileValidationCommand : WhileCommand
    {
        private readonly ISpawnValidation validation;
        public WhileValidationCommand(ISpawnValidation validation, params ISpawnSchedulerCommand[] commands) : base(commands)
        {
            this.validation = validation;
        }
        protected override bool CheckCondition(ISpawnSchedulerController scheduler)
        {
            return validation.Validate(scheduler.Context);
        }
    }
}