using System.Collections.Generic;

namespace MyInventory.Testing{

    public interface IStorage<IId, IValue>{
        IValue GetValue(IId id);
    }

    internal interface IStringStorage : IStorage<ushort, string>{ }
    internal interface IIconStorage : IStorage<ushort, UnityEngine.Sprite>{ }

    public abstract class ScriptableStringStorage : UnityEngine.ScriptableObject, IStringStorage
    {
        [UnityEngine.SerializeField] List<ItemString> itemStrings;
        private Dictionary<ushort, string> m_cache;

        void OnEnable(){
            if(m_cache == null){
                m_cache = new Dictionary<ushort, string>();

                for(int i = itemStrings.Count - 1; i >= 0; --i){
                    m_cache.TryAdd(itemStrings[i].Id, itemStrings[i].StringValue);
                }
            }
        }

        void OnDisable(){
            if(m_cache != null){
                m_cache = null;
            }
        }

        public string GetValue(ushort id){
            m_cache.TryGetValue(id, out string value);
            return value;
        }
    }
}