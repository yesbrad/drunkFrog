using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine.AI;

public class PencilPartyUtils
{
    public static int RoundUp(int numToRound, int multiple)
    {
        if (multiple == 0)
            return numToRound;

        int remainder = Mathf.Abs(numToRound) % multiple;
        if (remainder == 0)
            return numToRound;

        if (numToRound < 0)
            return -(Mathf.Abs(numToRound) - remainder);
        else
            return numToRound + multiple - remainder;
    }

	public static Vector3 RoundAnglesToNearest90(Transform rotationTranform)
	{
		Vector3 newAngle = new Vector3(0, Mathf.Round(rotationTranform.eulerAngles.y / 90) * 90, 0);

		if(newAngle.y == 360)
			newAngle.y = 0;

		return newAngle;
	}

    public static T Clone<T>(T source)
    {
        if (!typeof(T).IsSerializable)
        {
            throw new System.Exception("The type must be serializable.");
        }

        // Don't serialize a null object, simply return the default for that object
        if (UnityEngine.Object.ReferenceEquals(source, null))
        {
            return default(T);
        }

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new MemoryStream();
        using (stream)
        {
            formatter.Serialize(stream, source);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(stream);
        }
    }

    [MenuItem("Pencil Party Utils/Update ItemSpawner Names")]
    public static void UpdateItemSpawnerNames()
    {
        ItemSceneSpawner[] spawners = GameObject.FindObjectsOfType<ItemSceneSpawner>();

        foreach (ItemSceneSpawner item in spawners)
        {
            if(item.itemData != null)
            {
                item.name = $"ITEM: {item.itemData.name}";
            }
            else
            {
                item.name = "[MISSING ITEM DATA] ItemSpawner";
            }
        }
    }

    [MenuItem("Pencil Party Utils/Fit Selected Collider")]
    static void FitToChildren()
    {
        foreach (GameObject rootGameObject in Selection.gameObjects)
        {
            if (!(rootGameObject.GetComponent<Collider>() is BoxCollider))
                continue;

            bool hasBounds = false;
            Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

            for (int i = 0; i < rootGameObject.transform.childCount; ++i)
            {
                Renderer childRenderer = rootGameObject.transform.GetChild(i).GetComponent<Renderer>();
                if (childRenderer != null)
                {
                    if (hasBounds)
                    {
                        bounds.Encapsulate(childRenderer.bounds);
                    }
                    else
                    {
                        bounds = childRenderer.bounds;
                        hasBounds = true;
                    }
                }
            }

            BoxCollider collider = (BoxCollider)rootGameObject.GetComponent<Collider>();
            collider.center = bounds.center - rootGameObject.transform.position;
            collider.size = bounds.size;
        }
    }

    [MenuItem("Pencil Party Utils/Fit Selected NavMeshCarve")]
    static void FitToNav()
    {
        foreach (GameObject rootGameObject in Selection.gameObjects)
        {
            if (!(rootGameObject.GetComponent<NavMeshObstacle>() is NavMeshObstacle))
                continue;

            bool hasBounds = false;
            Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

            for (int i = 0; i < rootGameObject.transform.childCount; ++i)
            {
                Renderer childRenderer = rootGameObject.transform.GetChild(i).GetComponent<Renderer>();
                if (childRenderer != null)
                {
                    if (hasBounds)
                    {
                        bounds.Encapsulate(childRenderer.bounds);
                    }
                    else
                    {
                        bounds = childRenderer.bounds;
                        hasBounds = true;
                    }
                }
            }

            NavMeshObstacle collider = rootGameObject.GetComponent<NavMeshObstacle>();
            collider.center = bounds.center - rootGameObject.transform.position;
            collider.size = bounds.size;
        }
    }
}
