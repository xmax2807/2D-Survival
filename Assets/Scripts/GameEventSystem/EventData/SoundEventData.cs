namespace Project.GameEventSystem
{
    [System.Serializable]
    public struct SoundEventData
    {
        public int SoundId;
        public float Volume;

        public SoundEventData(int soundId, float volume){
            SoundId = soundId;
            Volume = volume;
        }
    }
}