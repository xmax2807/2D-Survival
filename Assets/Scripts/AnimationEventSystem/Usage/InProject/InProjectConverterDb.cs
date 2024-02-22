namespace Project.AnimationEventSystem.UsageInProject
{
    [UnityEngine.CreateAssetMenu(fileName = "InProject_ConverterDb", menuName = "AnimationEventSystem/InProject_ConverterDb")]
    public class InProjectConverterDb : ConverterDb
    {
        protected override void DefineConverters()
        {
            IEventDataConverter physicEventDataConverter = new PhysicEventDataConverter();
            IEventDataConverter soundEventDataConverter = new SoundEventDataConverter();
            m_converterMap.Add(id_MaterialDetectionEvent, physicEventDataConverter);
            m_converterMap.Add(id_SoundEvent, soundEventDataConverter);
        }
    }
}