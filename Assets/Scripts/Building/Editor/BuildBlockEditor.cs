using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BuildBlock))]
public class BuildBlockEditor : Editor
{
    BuildBlock block;
    public bool isEditing;

    private void Awake()
    {
        block = (BuildBlock)target;
    }

    public override void OnInspectorGUI()
    {
        SerializedProperty buildKit = serializedObject.FindProperty("buildKit");
        EditorGUILayout.PropertyField(buildKit);

        if(block.buildKit == null)
        {
            EditorGUILayout.HelpBox("Missing Build Kit. Please add one above.", MessageType.Error, true);
            serializedObject.ApplyModifiedProperties();
            return;
        }

        GUILayout.Space(10);
        GUILayout.Label("Walls");
        GUILayout.Space(10);

        SerializedProperty wall1 = serializedObject.FindProperty("Wall1");
        wall1.intValue = DrawBlockSlot(wall1.intValue, "Wall 1", block.buildKit.walls.Count);

        SerializedProperty wall2 = serializedObject.FindProperty("Wall2");
        wall2.intValue = DrawBlockSlot(wall2.intValue, "Wall 2", block.buildKit.walls.Count);

        SerializedProperty wall3 = serializedObject.FindProperty("Wall3");
        wall3.intValue = DrawBlockSlot(wall3.intValue, "Wall 3", block.buildKit.walls.Count);

        SerializedProperty wall4 = serializedObject.FindProperty("Wall4");
        wall4.intValue = DrawBlockSlot(wall4.intValue, "Wall 4", block.buildKit.walls.Count);

        GUILayout.Space(10);
        GUILayout.Label("Foundations");
        GUILayout.Space(10);

        SerializedProperty foundation1 = serializedObject.FindProperty("foundation1");
        foundation1.intValue = DrawBlockSlot(foundation1.intValue, "Foundation 1", block.buildKit.foundations.Count);

        SerializedProperty foundation2 = serializedObject.FindProperty("foundation2");
        foundation2.intValue = DrawBlockSlot(foundation2.intValue, "Foundation 2", block.buildKit.foundations.Count);

        SerializedProperty foundation3 = serializedObject.FindProperty("foundation3");
        foundation3.intValue = DrawBlockSlot(foundation3.intValue, "Foundation 3", block.buildKit.foundations.Count);

        SerializedProperty foundation4 = serializedObject.FindProperty("foundation4");
        foundation4.intValue = DrawBlockSlot(foundation4.intValue, "Foundation 4", block.buildKit.foundations.Count);

        if(GUILayout.Button("Remove Foundations"))
        {
            serializedObject.FindProperty("foundation1").intValue = 0;
            serializedObject.FindProperty("foundation2").intValue = 0;
            serializedObject.FindProperty("foundation3").intValue = 0;
            serializedObject.FindProperty("foundation4").intValue = 0;
        }

        GUILayout.Space(10);
        GUILayout.Label("Floor");
        GUILayout.Space(10);

        SerializedProperty floor = serializedObject.FindProperty("floors");
        floor.intValue = DrawBlockSlot(floor.intValue, "Floor", block.buildKit.floors.Count);
        
        GUILayout.Space(10);
        GUILayout.Label("Stairs");
        GUILayout.Space(10);

        SerializedProperty stairs = serializedObject.FindProperty("stairs");
        stairs.intValue = DrawBlockSlot(stairs.intValue, "Stairs", block.buildKit.stairs.Count);
       
        GUILayout.Space(10);

        if (GUILayout.Button("Rotate"))
        {
            block.Rotate();
        }

        if (GUILayout.Button("Repaint"))
        {
            block.Repaint();
        }

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
            EditorUtility.SetDirty(block);
    }

    public int DrawBlockSlot(int slot, string name, int slotLength)
    {
        int newSlot = slot;

        GUILayout.BeginHorizontal();
        GUILayout.Label($"{name}:  {slot}");

        if (GUILayout.Button("+"))
        {
            newSlot = Clamp(slot + 1, slotLength);
        }

        if (GUILayout.Button("-"))
        {
            newSlot = Clamp(slot - 1, slotLength);
        }

        GUILayout.EndHorizontal();

        return newSlot;
    }

    public int Clamp(int main, int length)
    {
        if (main > length - 1)
        {
            return length;
        }

        if (main <= 0)
        {
            return 0;
        }

        return main;
    }

    void OnEnable()
    {
        SceneView.onSceneGUIDelegate += this.OnSceneGUI;
    }

    void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
    }

    public void OnSceneGUI(SceneView sceneView)
    {
        Handles.color = Color.cyan;
        Handles.DrawLine(Vector3.zero, Vector3.up * 200);

        //Handles.DrawWireCube(block.transform.position  , Vector3.one * 2);
    }
}
