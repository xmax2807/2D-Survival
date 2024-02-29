using UnityEngine;

namespace Project.SaveSystem
{
    /// <summary>
    /// Using JsonUtility
    /// </summary>
    public class NativeJsonSerializer : IJsonSerializer
    {
        public T Deserialize<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public string Serialize<T>(T obj)
        {
            return JsonUtility.ToJson(obj);
        }
    }
}