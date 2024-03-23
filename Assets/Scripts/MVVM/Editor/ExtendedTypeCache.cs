using UnityEditor;
#if UNITY_EDITOR
using MVVMToolkit.Editor.TypeSerialization;
namespace Project.MVVM
{
    public static class ExtendedTypeCache
    {
        [InitializeOnLoadMethod]
        static void OnProjectLoadedInEditor()
        {
            TypeCaching.CacheTypes(typeof(IItemCollectionBinder));
            TypeCaching.CacheTypes(typeof(ICollectionBinder));
        }
    }
}
#endif