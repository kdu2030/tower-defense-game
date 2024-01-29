using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PrefabAdder : EditorWindow {
    public GameObject[] prefabs;
    public SerializedProperty prefabsSerializedProperty;
    public SerializedObject serializedObject;

    private GameObject parentGameObject;
    private Dictionary<string, GameObject> spriteNameToPrefab;



    [MenuItem("Window/Prefabs/Prefab Adder")]
    public static void ShowTerrainModifier() {
        EditorWindow window = GetWindow<PrefabAdder>();
        GUIContent content = new GUIContent("Terrain Modifier");
        window.titleContent = content;
    }

    private void OnEnable() {
        serializedObject = new SerializedObject(this);
        prefabsSerializedProperty = serializedObject.FindProperty("prefabs");
        spriteNameToPrefab = new Dictionary<string, GameObject>();
    }

    private bool IsSubmitDisabled() {
        if (parentGameObject == null || prefabs == null || prefabs.Length <= 0) {
            return true;
        }

        foreach (GameObject prefab in prefabs) {
            if (prefab == null) {
                return true;
            }
        }

        return false;
    }

    private void PopulatePrefabDictionary() {
        foreach (GameObject prefab in prefabs) {
            SpriteRenderer spriteRenderer = prefab.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) {
                continue;
            }
            spriteNameToPrefab.Add(spriteRenderer.sprite.name, prefab);
        }
    }

    private void FindAndAddPrefab(Transform childObjectTransform) {
        GameObject childObject = childObjectTransform.gameObject;
        SpriteRenderer spriteRenderer = childObject.GetComponent<SpriteRenderer>();

        GameObject prefab = spriteNameToPrefab[spriteRenderer.sprite.name];
        if (prefab == null) {
            return;
        }

        //TODO: Instantiate and Destroy here
    }



    private void AddPrefabsToChildren() {
        Transform[] childrenTransforms = parentGameObject.GetComponentsInChildren<Transform>();
        if (prefabs.Length == 0 || childrenTransforms.Length == 0) {
            throw new System.Exception("Terrain Parent must have children and Prefab List must be populated");
        }

        PopulatePrefabDictionary();

        foreach (Transform childTransform in childrenTransforms) {

        }
    }



    public void OnGUI() {
        serializedObject.Update();
        parentGameObject = EditorGUILayout.ObjectField("Parent Object", parentGameObject, typeof(GameObject), true) as GameObject;

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(prefabsSerializedProperty);

        EditorGUILayout.Space();

        EditorGUI.BeginDisabledGroup(IsSubmitDisabled());
        GUILayout.Button("Add Prefabs to Children");
        EditorGUI.EndDisabledGroup();


        serializedObject.ApplyModifiedProperties();
    }

}
