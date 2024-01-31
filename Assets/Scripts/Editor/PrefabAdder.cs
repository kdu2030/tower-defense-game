using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;

public class PrefabAdder : EditorWindow {
    public GameObject[] prefabs;
    public SerializedProperty prefabsSerializedProperty;
    public SerializedObject serializedObject;

    private GameObject parentGameObject;
    private Dictionary<int, GameObject> spriteHashToPrefab;



    [MenuItem("Window/Prefabs/Prefab Adder")]
    public static void ShowTerrainModifier() {
        EditorWindow window = GetWindow<PrefabAdder>();
        GUIContent content = new GUIContent("Terrain Modifier");
        window.titleContent = content;
    }

    private void OnEnable() {
        serializedObject = new SerializedObject(this);
        prefabsSerializedProperty = serializedObject.FindProperty("prefabs");
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
        spriteHashToPrefab = new Dictionary<int, GameObject>();

        foreach (GameObject prefab in prefabs) {
            SpriteRenderer spriteRenderer = prefab.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) {
                continue;
            }
            spriteHashToPrefab.Add(spriteRenderer.sprite.GetHashCode(), prefab);
        }
    }

    private void FindAndAddPrefab(Transform childObjectTransform) {
        GameObject childObject = childObjectTransform.gameObject;
        SpriteRenderer spriteRenderer = childObject.GetComponent<SpriteRenderer>();
        if (childObject.IsPrefabInstance() || spriteRenderer == null) {
            return;
        }

        GameObject prefab;
        bool foundPrefab = spriteHashToPrefab.TryGetValue(spriteRenderer.sprite.GetHashCode(), out prefab);

        if (!foundPrefab) {
            return;
        }

        ConvertToPrefabInstanceSettings settings = new ConvertToPrefabInstanceSettings();
        PrefabUtility.ConvertToPrefabInstance(childObject, prefab, settings, InteractionMode.UserAction);
    }



    private void AddPrefabsToChildren() {
        // Note - GetComponentsInChildren also returns the transform of the parent
        Transform[] childrenTransforms = parentGameObject.GetComponentsInChildren<Transform>();

        if (prefabs.Length == 0 || childrenTransforms.Length == 0) {
            throw new Exception("Terrain Parent must have children and Prefab List must be populated");
        }

        PopulatePrefabDictionary();

        foreach (Transform childTransform in childrenTransforms) {
            FindAndAddPrefab(childTransform);
        }
    }



    public void OnGUI() {
        serializedObject.Update();
        parentGameObject = EditorGUILayout.ObjectField("Parent Object", parentGameObject, typeof(GameObject), true) as GameObject;

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(prefabsSerializedProperty);

        EditorGUILayout.Space();

        EditorGUI.BeginDisabledGroup(IsSubmitDisabled());

        if (GUILayout.Button("Add Prefabs to Children")) {
            AddPrefabsToChildren();
        };

        EditorGUI.EndDisabledGroup();


        serializedObject.ApplyModifiedProperties();
    }

}
