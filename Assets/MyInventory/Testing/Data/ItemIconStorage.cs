using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace MyInventory.Testing{

    [CreateAssetMenu(fileName = "ItemIconStorage", menuName = "MyInventory/ItemIconStorage")]
    public class ItemIconStorage : ScriptableObject, IIconStorage
    {        
        [SerializeField] SpriteAtlas m_atlas;
        [SerializeField] string prefix;

        private readonly static System.Text.StringBuilder s_builder = new(8); 

        readonly Dictionary<ushort,Sprite> m_cache = new();

        public Sprite GetValue(ushort id)
        {
            if(m_cache.TryGetValue(id, out Sprite value)){
                return value;
            }
            else{
                string result = s_builder.Clear().Append(prefix).Append(id).ToString();
                Sprite sprite = m_atlas.GetSprite(result);
                m_cache.TryAdd(id, sprite);

                return sprite;
            }
        }
    }
}