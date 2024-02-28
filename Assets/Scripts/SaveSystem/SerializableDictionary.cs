using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace Project.SaveSystem
{
    [System.Serializable, MessagePackObject]
    public class SerializableDictionary<TKey, TValue>
    {

        [Key(0)]
        public List<TKey> keys;
        [Key(1)]
        public List<TValue> values;
        public TValue this[TKey key] => values[keys.IndexOf(key)];


        public SerializableDictionary(){
            keys = new List<TKey>();
            values = new List<TValue>();
        }

        public SerializableDictionary(Dictionary<TKey, TValue> dict){
            keys = new List<TKey>();
            values = new List<TValue>();

            foreach (KeyValuePair<TKey, TValue> kvp in dict){
                Add(kvp.Key, kvp.Value);
            }
        }

        public void Add(TKey key, TValue value){
            keys.Add(key);
            values.Add(value);
        }

        public void Remove(TKey key){
            int keyIndex = keys.IndexOf(key);
            keys.RemoveAt(keyIndex);
            values.RemoveAt(keyIndex);
        }

        public Dictionary<TKey, TValue> ToDictionary(){
            Dictionary<TKey, TValue> dict = new();
            for (int i = keys.Count - 1; i >= 0; --i){
                dict.Add(keys[i], values[i]);
            }
            return dict;
        }

        public void SetData(Dictionary<TKey, TValue> dict){
            keys.Clear();
            values.Clear();
            foreach (KeyValuePair<TKey, TValue> kvp in dict){
                Add(kvp.Key, kvp.Value);
            }
        }
    }
}