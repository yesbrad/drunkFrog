using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[SelectionBase]
[ExecuteInEditMode]
public class BuildBlock : MonoBehaviour
{
    [Serializable]
    public class BuildSlot
    {
        public GameObject buildItem;
        public int id;

        private GameObject previousBuildItem;

        public BuildSlot()
        {
            buildItem = null;
            id = 0;
        }

        public void Update(int index, GameObject newItem, Vector3 position, float angle, Transform parent)
        {
            if(index == 0)
            {
                previousBuildItem = buildItem;

                UnityEditor.EditorApplication.delayCall += () =>
                {
                    print("DELTING");
                    DestroyImmediate(previousBuildItem);
                };

                buildItem = null;
                id = index;
                return;
            }

            previousBuildItem = buildItem;

            UnityEditor.EditorApplication.delayCall += () =>
            {
                DestroyImmediate(previousBuildItem);
            };

            buildItem = null;
            buildItem = Instantiate(newItem, position, Quaternion.Euler(-90,angle,0));
            buildItem.transform.parent = parent;
            buildItem.isStatic = true;

            id = index;
        }

        public bool CheckForUpdate (int index)
        {
            return id != index;
        }
    }

    public BuildKit buildKit;

    [Header("Walls")]
    public int Wall1;
    public int Wall2;
    public int Wall3;
    public int Wall4;

    [Header("Fountations")]
    public int foundation1;
    public int foundation2;
    public int foundation3;
    public int foundation4;

    [Header("Extras")]
    public int floors = 1;
    public int stairs;

    public BuildSlot floorSlot = new BuildSlot();

    public BuildSlot wall1Slot = new BuildSlot();
    public BuildSlot wall2Slot = new BuildSlot();
    public BuildSlot wall3Slot = new BuildSlot();
    public BuildSlot wall4Slot = new BuildSlot();

    public BuildSlot foundation1Slot = new BuildSlot();
    public BuildSlot foundation2Slot = new BuildSlot();
    public BuildSlot foundation3Slot = new BuildSlot();
    public BuildSlot foundation4Slot = new BuildSlot();

    public BuildSlot stairSlot = new BuildSlot();

    private int currentRotation = 0;

    private void OnValidate()
    {
        if(buildKit == null)
        {
            Debug.LogWarning("Build Block Missing buildKit", gameObject);
            return;
        }

        if (stairSlot.CheckForUpdate(stairs))
        {
            stairSlot.Update(stairs, buildKit.stairs[ClampIndex(stairs - 1)], GetCenterPosition(), currentRotation + 0, transform);
        }

        // Clamp Floors
        if (floorSlot.CheckForUpdate(floors))
        {
            floorSlot.Update(floors, buildKit.floors[ClampIndex(floors - 1)], GetCenterPosition(), currentRotation + 0, transform);
        }

        CheckWalls();
        CheckFoundations();
    }

    public void Repaint()
    {
        stairSlot.Update(stairs, buildKit.stairs[ClampIndex(stairs - 1)], GetCenterPosition(), currentRotation + 0, transform);
        floorSlot.Update(floors, buildKit.floors[ClampIndex(floors - 1)], GetCenterPosition(), currentRotation + 0, transform);
        foundation1Slot.Update(foundation1, buildKit.foundations[ClampIndex(foundation1 - 1)], GetCenterPosition(), currentRotation + 0, transform);
        foundation2Slot.Update(foundation2, buildKit.foundations[ClampIndex(foundation2 - 1)], GetCenterPosition(), currentRotation + 90, transform);
        foundation3Slot.Update(foundation3, buildKit.foundations[ClampIndex(foundation3 - 1)], GetCenterPosition(), currentRotation + 180, transform);
        foundation4Slot.Update(foundation4, buildKit.foundations[ClampIndex(foundation4 - 1)], GetCenterPosition(), currentRotation + -90, transform);
        wall1Slot.Update(Wall1, buildKit.walls[ClampIndex(Wall1 - 1)], GetCenterPosition(), currentRotation + 0, transform);
        wall2Slot.Update(Wall2, buildKit.walls[ClampIndex(Wall2 - 1)], GetCenterPosition(), currentRotation + 90, transform);
        wall3Slot.Update(Wall3, buildKit.walls[ClampIndex(Wall3 - 1)], GetCenterPosition(), currentRotation + 180, transform);
        wall4Slot.Update(Wall4, buildKit.walls[ClampIndex(Wall4 - 1)], GetCenterPosition(), currentRotation + -90, transform);
    }

    public void Rotate()
    {
        currentRotation += 90;

        if(currentRotation == 360)
        {
            currentRotation = 0;
        }

        Repaint();
    }

    public void CheckFoundations ()
    {
        if (foundation1Slot.CheckForUpdate(foundation1))
        {
            foundation1Slot.Update(foundation1, buildKit.foundations[ClampIndex(foundation1 - 1)], GetCenterPosition(), currentRotation + 0, transform);
        }

        if (foundation2Slot.CheckForUpdate(foundation2))
        {
            foundation2Slot.Update(foundation2, buildKit.foundations[ClampIndex(foundation2 - 1)], GetCenterPosition(), currentRotation + 90, transform);
        }

        if (foundation3Slot.CheckForUpdate(foundation3))
        {
            foundation3Slot.Update(foundation3, buildKit.foundations[ClampIndex(foundation3 - 1)], GetCenterPosition(), currentRotation + 180, transform);
        }

        if (foundation4Slot.CheckForUpdate(foundation4))
        {
            foundation4Slot.Update(foundation4, buildKit.foundations[ClampIndex(foundation4 - 1)], GetCenterPosition(), currentRotation + 270, transform);
        }

    }

    private void CheckWalls()
    {
        // Walls Floors
        if (wall1Slot.CheckForUpdate(Wall1))
        {
            wall1Slot.Update(Wall1, buildKit.walls[ClampIndex(Wall1 - 1)], GetCenterPosition(), currentRotation + 0, transform);
        }

        if (wall2Slot.CheckForUpdate(Wall2))
        {
            wall2Slot.Update(Wall2, buildKit.walls[ClampIndex(Wall2 - 1)], GetCenterPosition(), currentRotation + 90, transform);
        }

        if (wall3Slot.CheckForUpdate(Wall3))
        {
            wall3Slot.Update(Wall3, buildKit.walls[ClampIndex(Wall3 - 1)], GetCenterPosition(), currentRotation + 180, transform);
        }

        if (wall4Slot.CheckForUpdate(Wall4))
        {
            wall4Slot.Update(Wall4, buildKit.walls[ClampIndex(Wall4 - 1)], GetCenterPosition(), currentRotation + 270, transform);
        }
    }

    private int ClampIndex (int i)
    {
        if (i < 0)
            return 0;

        return i;
    }

    private Vector3 GetCenterPosition()
    {
        return transform.position + new Vector3(1, 0, 1);
    }
}
