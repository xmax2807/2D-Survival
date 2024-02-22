using UnityEngine;
namespace Project.GameParams
{
    [System.Serializable]
    public class MaterialParam{
        [field: SerializeField] public Enums.MaterialType MaterialType {get;private set;}
        [field: SerializeField] public int StepSoundId {get; private set;}
    }
}