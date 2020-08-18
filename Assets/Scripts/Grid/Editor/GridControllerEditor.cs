using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridController))]
public class GridControllerEditor : Editor
{
    GridController cont;

    private void Awake()
    {
        cont = (GridController)target;
    }

    public override void OnInspectorGUI()
    {
        // If you want to display THE_CLASS_YOU_DERIVE_FROM as it is usually on the inspector (doesn't completely work with some components that have "complex" editor)
        base.OnInspectorGUI();

        GUILayout.Label($"Has Grid: {cont.grid.initilized}");
        
        if(cont.grid.GridArray != null)
        {

        GUILayout.Label($"Grid Length: { cont.grid.GridArray.Length}");
        }
        else
        {
            GUILayout.Label($"Grid Array Is Null");
        }

        if (GUILayout.Button("Create Grid"))
        {
            cont.InitGrid();
        }

        if (GUI.changed)
            EditorUtility.SetDirty(cont);

        // Otherwise feel free to do what you want
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
        Handles.color = Color.white;
        Handles.DrawLine(Vector3.zero, Vector3.up * 200);
        
        for (int x = 0; x < cont.gridSizeY; x++)
        {
            for (int y = 0; y < cont.gridSizeX; y++)
            {
                Handles.DrawLine(cont.grid.GetWorldPositionFromGrid(x, y + 1), cont.grid.GetWorldPositionFromGrid(x, y));
                Handles.DrawLine(cont.grid.GetWorldPositionFromGrid(x + 1, y), cont.grid.GetWorldPositionFromGrid(x, y));
            }
        }

        Debug.DrawLine(cont.grid.GetWorldPositionFromGrid(cont.grid.width, cont.grid.height), cont.grid.GetWorldPositionFromGrid(cont.grid.width, 0));
        Debug.DrawLine(cont.grid.GetWorldPositionFromGrid(cont.grid.width, cont.grid.height), cont.grid.GetWorldPositionFromGrid(0, cont.grid.height));
    }
}
