﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(GridController))]
public class GridControllerEditor : Editor
{
    GridController cont;
    public bool isEditing;

    bool isConfirm;

    private void Awake()
    {
        cont = (GridController)target;
    }

    public override void OnInspectorGUI()
    {
        // If you want to display THE_CLASS_YOU_DERIVE_FROM as it is usually on the inspector (doesn't completely work with some components that have "complex" editor)
        base.OnInspectorGUI();

        if(cont.grid.initilized == false)
        {
            if (GUILayout.Button("Create Grid"))
            {
                cont.RepaintGrid();
            }
        }
        else
        {
            if (GUILayout.Button(isEditing ? "Stop Editing" : "Edit Grid"))
            {
                isEditing = !isEditing;
            }

            if (isConfirm)
            {

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Do the dam Repaint (DESTRUCTIVE)"))
                {
                    cont.RepaintGrid();
                    isConfirm = false;
                }
                if (GUILayout.Button("Go Back"))
                {
                    isConfirm = false;
                }
                GUILayout.EndHorizontal();
            }
            else
            {
                if (GUILayout.Button("Repaint (DESTRUCTIVE)"))
                {
                    isConfirm = true;
                }
            }
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

    public void OnSceneGUI(SceneView yurt)
    {
        //Handles.color = Color.red;
        
        for (int x = 0; x < cont.gridSizeY; x++)
        {
            for (int y = 0; y < cont.gridSizeX; y++)
            {
                Handles.DrawLine(cont.grid.GetWorldPositionFromGrid(x, y + 1), cont.grid.GetWorldPositionFromGrid(x, y));
                Handles.DrawLine(cont.grid.GetWorldPositionFromGrid(x + 1, y), cont.grid.GetWorldPositionFromGrid(x, y));

                if (isEditing)
                {
                    Vector3 pos = cont.grid.GetWorldGridCenterPositionFromGrid(x, y);
                    GridSlot newSlot = cont.grid.GridArray[cont.grid.GetGridOneDIndex(x, y)];

                    //Handles.color = newSlot.gridState == GridSlotState.Blocked ? Color.red : Color.green;

                    if (Handles.Button(pos, Quaternion.identity, newSlot.gridState == GridSlotState.Blocked ? 2f : 1.3f, 1.5f, Handles.CubeHandleCap))
                    {
                        newSlot.gridState = newSlot.gridState == GridSlotState.Blocked ? GridSlotState.Open : GridSlotState.Blocked;
                    }

                    Handles.color = Color.red;
                }
            }
        }

        //Handles.color = Color.red;
        Debug.DrawLine(cont.grid.GetWorldPositionFromGrid(cont.grid.width, cont.grid.height), cont.grid.GetWorldPositionFromGrid(cont.grid.width, 0));
        Debug.DrawLine(cont.grid.GetWorldPositionFromGrid(cont.grid.width, cont.grid.height), cont.grid.GetWorldPositionFromGrid(0, cont.grid.height));
    }
}
