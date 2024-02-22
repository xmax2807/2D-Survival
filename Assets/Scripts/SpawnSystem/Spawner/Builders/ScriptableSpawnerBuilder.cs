using UnityEngine;

namespace Project.SpawnSystem
{
    public abstract class ScriptableSpawnerBuilder : ScriptableObject
    {
        public abstract IIndividualSpawnerBuilder GetIndividualBuilder();
    }
}