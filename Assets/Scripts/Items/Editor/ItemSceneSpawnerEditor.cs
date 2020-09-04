using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemSceneSpawner))]
public class ItemSceneSpawnerEditor : Editor
{
    ItemSceneSpawner block;
    private void Awake()
    {
        block = (ItemSceneSpawner)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
