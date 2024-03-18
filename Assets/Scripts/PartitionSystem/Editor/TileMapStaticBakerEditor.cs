#if UNITY_EDITOR
using Project.PartitionSystem;
namespace UnityEditor
{
    [CustomEditor(typeof(TileMapStaticBaker))]
    public class TileMapStaticBakerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            TileMapStaticBaker myScript = (TileMapStaticBaker)target;
            if (UnityEngine.GUILayout.Button("Bake Tiles"))
            {
                Undo.RecordObject(myScript.TileMap, "Bake Tiles");
                myScript.Bake();

            }
        }
    }
}
#endif