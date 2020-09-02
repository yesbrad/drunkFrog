using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BuildLevelUtility))]
public class BuildLevelUtilityEditor : Editor
{
    BuildLevelUtility block;
    public bool isEditing;

    private void Awake()
    {
        block = (BuildLevelUtility)target;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("ReLayer"))
        {
            block.UpdateBlockLayer();
        }
    }
}
