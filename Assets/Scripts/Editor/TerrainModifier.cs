using UnityEditor;
using UnityEngine;

public class TerrainModifier : EditorWindow {
    private GameObject terrainParent;
    public GameObject[] prefabs;
    public SerializedProperty prefabsSerializedProperty;
    public SerializedObject serializedObject;

    [MenuItem("Window/Terrain/Terrain Modifier")]
    public static void ShowTerrainModifier() {
        EditorWindow window = GetWindow<TerrainModifier>();
        GUIContent content = new GUIContent("Terrain Modifier");
        window.titleContent = content;
    }

    private void OnEnable() {
        serializedObject = new SerializedObject(this);
        prefabsSerializedProperty = serializedObject.FindProperty("prefabs");
    }


    public void OnGUI() {
        serializedObject.Update();
        terrainParent = EditorGUILayout.ObjectField("Terrain Parent Object", terrainParent, typeof(GameObject), true) as GameObject;

        EditorGUILayout.Space();

        EditorGUI.BeginDisabledGroup(terrainParent == null);
        GUILayout.Button("Add Components to Children");
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.PropertyField(prefabsSerializedProperty);
        serializedObject.ApplyModifiedProperties();


    }

}
