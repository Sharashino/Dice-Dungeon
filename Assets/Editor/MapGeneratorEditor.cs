using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGenerator = (MapGenerator)target;

        if (GUILayout.Button("Generate Map")) 
            mapGenerator.GenerateMap();
        
        if (GUILayout.Button("Destroy Map"))
            mapGenerator.DestroyOldMap();

        DrawDefaultInspector();
    }
}
