using System;
using System.Collections.Generic;
using MVVMToolkit.TypeSerialization;
using UnityEngine;

namespace Project.MVVM
{
    internal static class CollectionBinderMap
    {
        private static readonly Dictionary<Type, Type> Map;

        static CollectionBinderMap()
        {
            Map = new();
            foreach (var type in GetTypes(typeof(ICollectionBinder)))
            {
                var instance = (ICollectionBinder)Activator.CreateInstance(type);
                Map.Add(instance.Type, type);
            }
        }

        public static ICollectionBinder GetBinder(Type viewType) =>
            (ICollectionBinder)Activator.CreateInstance(Map[viewType]);

        private static List<Type> GetTypes(Type derivingType)
        {
            var result = new List<Type>();
            var assets = Resources.LoadAll<TextAsset>($"MVVMToolkit/TypeCache/{derivingType.Name}");
            if (assets is null)
            {
                return result;
            }

            foreach (var textAsset in assets)
            {
                var types = JsonUtility.FromJson<SerializedTypes>(textAsset.text);
                //Get this class assembly
                var assembly = typeof(CollectionBinderMap).Assembly;
                foreach (var typeName in types.fullTypeNames)
                {
                    result.Add(assembly.GetType(typeName));
                }
            }

            return result;
        }
    }
}