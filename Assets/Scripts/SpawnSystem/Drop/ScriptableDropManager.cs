using UnityEngine;

namespace Project.SpawnSystem
{
    public abstract class ScriptableDropManager : ScriptableObject
    {
        public abstract IDropObservable DropObservable {get;}
    }
}