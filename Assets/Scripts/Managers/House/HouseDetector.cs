using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseDetector : MonoBehaviour
{
    private HouseManager manager;

    private void Awake()
    {
        manager = GetComponentInParent<HouseManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (manager != null)
            {
                other.GetComponentInParent<CharacterManager>().SetHouse(manager);
            }
            else
            {
                Debug.LogError("Missing House Manager. Please Help", gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position + Vector3.up * 50, Vector3.one);

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
