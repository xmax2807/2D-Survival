namespace Project.GameFlowSystem
{
    public abstract class ScriptableGameStateBuilder : UnityEngine.ScriptableObject, IGameStateBuilder
    {
        /// <summary>
        /// This field must be consistent so that other class can identify this builder, for example, SequenceData.TargetBuilder == this string
        /// </summary>
        [UnityEngine.SerializeField] private string identificationName;
        public string IdentificationName => identificationName;
        public abstract IGameState BuildState(SequenceData data, CommandProvider commandProvider);
    }
}