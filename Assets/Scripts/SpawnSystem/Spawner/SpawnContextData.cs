namespace Project.SpawnSystem
{

    /// <summary>
    /// This class will hold configured data for spawn context: start position, shape formation, how many entities needs to spawn,...
    /// </summary>
    [System.Serializable]
    public class SpawnContextData
    {
        public uint SpawnCount;
        public SpawnPosition SpawnPosition;
        //TODO:Add start position and shape formation
    }
}