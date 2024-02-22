using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.SpawnSystem
{
    [CreateAssetMenu(fileName = "BuilderDirector", menuName = "SpawnSystem/BuilderDirector")]
    public class ScriptableBuilderDirector : ScriptableObject, ISpawnBuildDirector
    {
        [SerializeField] private ScriptableSpawnerBuilder m_scriptableSpawnerBuilder;
        [SerializeField] private ScriptableDropManager m_scriptableDropManager;
        
        public ISpawner BuildSpawner(SpawnType spawnType, SpawnData data, IStorageGetter storageGetter)
        {
            if(storageGetter == null || data == null){
                throw new System.ArgumentNullException("StorageGetter or SpawnData can't be null");
            }

            switch (spawnType)
            {
                case SpawnType.Regular: return BuildIndividualSpawner(data, m_scriptableSpawnerBuilder.GetIndividualBuilder(), storageGetter);
                case SpawnType.Wave:
                    break;
                case SpawnType.Boss:
                    break;
            }
            return null;
        }

        private ISpawner BuildIndividualSpawner(SpawnData data, IIndividualSpawnerBuilder builder, IStorageGetter storageGetter)
        {
            builder.Reset();
            builder.Result.SetContext(BuildContextForIndividual(data));
            builder.AddSpawnEntities(data.IndividualData, storageGetter);
            builder.AddDropObservable(m_scriptableDropManager.DropObservable);
            return builder.Result.CastToSpawner();
        }

        private ISpawnContext BuildContextForIndividual(SpawnData data){
            //not implemented
            SpawnerContext context = new SpawnerContext();
            context.SetAvailableSpawnIds(data.IndividualData.Select(x => x.EntityID).ToArray());
            context.AddSpawnRates(data.IndividualData.Select(x => x.SpawnRate).ToArray());
            context.SetCount(data.ContextData.SpawnCount);
            return context;
        }


        private ISpawnSchedulerCommand prepareCommand;
        private ISpawnSchedulerCommand spawnCommand;

        public ICommandSelector BuildSpawnCommandSelector(SpawnScheduleData data)
        {
            ICommandSelector result = new QueueCommandSelector();
            prepareCommand ??= new SpawnPrepareCommand();
            spawnCommand ??= new SpawnCommand();

            switch (data.TriggerType){
                case SpawnScheduleData.SpawnTriggerType.TimeBased:{
                    result.SetNextCommand(new WaitCommand(data.SpawnTime, null));
                }
                break;
                case SpawnScheduleData.SpawnTriggerType.EventBased:{
                    throw new System.NotImplementedException();
                }
            }

            if(data.HasRepeat){
                // build repeat command with pause validation
                var repeatCommand = WhileCommand.Create(BuildPauseValidation(data.RepeatCondition),
                        prepareCommand, spawnCommand, new WaitCommand(data.IntervalTime, null)
                    );

                // build while command wraps repeat command with stop condition
                result.SetNextCommand(
                    WhileCommand.Create(()=>true, repeatCommand)
                );
            }

            return result;
        }
        private ISpawnValidation BuildPauseValidation(SpawnCondition pauseCondition){
            if(pauseCondition == null){
                return null;
            }

            if(pauseCondition.SpawnConditionType == SpawnCondition.ConditionType.LimitActveCount){
                return new ActiveCountLimitValidation((uint)pauseCondition.Value);
            }
            return null;
        }
    }
}