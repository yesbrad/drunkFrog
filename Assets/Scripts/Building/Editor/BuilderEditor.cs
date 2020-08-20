using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuilderEditor : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/House Builder")]
    static void Init()
    {
        BuilderEditor window = (BuilderEditor)EditorWindow.GetWindow(typeof(BuilderEditor));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("House Builder", EditorStyles.boldLabel);

        if (GUILayout.Button("Rotate"))
        {
            GameObject[] o = Selection.gameObjects;

            for (int i = 0; i < o.Length; i++)
            {
                BuildBlock block = o[i].GetComponent<BuildBlock>();
                block.Rotate();
            }
        }

        if (GUILayout.Button("Repaint"))
        {
            GameObject[] o =  Selection.gameObjects;

            for (int i = 0; i < o.Length; i++)
            {
                BuildBlock block = o[i].GetComponent<BuildBlock>();
                block.Repaint();    
            }
        }
    }
}
