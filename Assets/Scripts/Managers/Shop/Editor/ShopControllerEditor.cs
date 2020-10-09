using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(ShopController))]
class ShopControllerEditor : Editor
{
    void OnEnable()
    {
        SceneView.onSceneGUIDelegate += this.OnSceneGUI;
    }

    void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
    }

    void OnSceneGUI(SceneView sceneView)
    {
        ShopController handleExample = (ShopController)target;

        if (handleExample == null)
        {
            return;
        }

        Handles.color = Color.blue;
       // Handles.CubeHandleCap(0, handleExample.transform.position, Quaternion.identity, 5, EventType.Repaint);

        Handles.Label(handleExample.transform.position + Vector3.up, handleExample.shopData.item.name);
        SceneView.RepaintAll();
    }
}