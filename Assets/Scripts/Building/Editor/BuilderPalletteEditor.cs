using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using System.ComponentModel;

public class BuilderPalletteEditor : EditorWindow
{
    int currentlySelected = 0;
    bool inPalletteMode;

    public BuildCatalog catalog;
    public Transform floorParent;

    GameObject prevModel;
    int prevIndex;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/House Builder")]
    static void Init()
    {
        BuilderPalletteEditor window = (BuilderPalletteEditor)EditorWindow.GetWindow(typeof(BuilderPalletteEditor));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("House Builder", EditorStyles.boldLabel);

        SerializedProperty a = new SerializedObject(this).FindProperty("catalog");
        EditorGUILayout.PropertyField(a); //Drawn correctly

        SerializedProperty b = new SerializedObject(this).FindProperty("floorParent");
        EditorGUILayout.PropertyField(b); //Drawn correctly

        if(floorParent == null)
        {
            EditorGUILayout.HelpBox("Missing Floor Parent", MessageType.Error);
        }

        if (catalog != null)
        {
            List<Texture> select = new List<Texture>();

            for (int i = 0; i < catalog.blocks.Length; i++)
            {
                //GUILayout.Label(catalog.blocks[i].name);
                //GUILayout.Box(AssetPreview.GetAssetPreview(catalog.blocks[i]));
                select.Add(AssetPreview.GetAssetPreview(catalog.blocks[i]));
               
            }

            currentlySelected = GUILayout.SelectionGrid(currentlySelected, select.ToArray(), 2);

        }

        if (floorParent != null)
        {
            
            if (GUILayout.Button(inPalletteMode ? "Exit Pallette Mode" : "Pallette Mode")) { 
                inPalletteMode = !inPalletteMode;
            }
        }

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

        a.serializedObject.ApplyModifiedProperties();
        b.serializedObject.ApplyModifiedProperties();
    }

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
        if (!inPalletteMode)
        {
            if(prevModel != null)
            {
                DestroyImmediate(prevModel);
                prevModel = null;
            }

            return;
        }

        // This will have scene events including mouse down on scenes objects
        Event cur = Event.current;
        Selection.activeGameObject = null;


        if(prevIndex != currentlySelected || prevModel == null)
        {
            if(prevModel)
                DestroyImmediate(prevModel);
            
            prevModel = null;
            prevModel = Instantiate(catalog.blocks[currentlySelected]);
            prevIndex = currentlySelected;
        }

        Vector3 mousePoss = cur.mousePosition;
        float pppp = EditorGUIUtility.pixelsPerPoint;
        mousePoss.y = sceneView.camera.pixelHeight - mousePoss.y * pppp;
        mousePoss.x *= pppp;

        Ray rayy = sceneView.camera.ScreenPointToRay(mousePoss);
        RaycastHit hitt;

        if (Physics.Raycast(rayy, out hitt, 10000f, ~(1 << LayerMask.NameToLayer("HouseTrigger"))))
        {
            prevModel.transform.position = new Vector3(PencilPartyUtils.RoundUp(Mathf.FloorToInt(hitt.point.x), 2), PencilPartyUtils.RoundUp(Mathf.FloorToInt(hitt.point.y), 2), PencilPartyUtils.RoundUp(Mathf.FloorToInt(hitt.point.z), 2));
        }

        if (cur.type == EventType.MouseDown && cur.button == 0)
        {

            Vector3 mousePos = cur.mousePosition;
            float ppp = EditorGUIUtility.pixelsPerPoint;
            mousePos.y = sceneView.camera.pixelHeight - mousePos.y * ppp;
            mousePos.x *= ppp;

            Ray ray = sceneView.camera.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10000f, ~(1 << LayerMask.NameToLayer("HouseTrigger"))))
            {
                GameObject a = Instantiate(catalog.blocks[currentlySelected], new Vector3(PencilPartyUtils.RoundUp(Mathf.FloorToInt(hit.point.x), 2), PencilPartyUtils.RoundUp(Mathf.FloorToInt(hit.point.y), 2), PencilPartyUtils.RoundUp(Mathf.FloorToInt(hit.point.z), 2)), Quaternion.identity);
                a.transform.parent = floorParent.transform;
                cur.Use();
            }

        }
    }
}
