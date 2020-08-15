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
}
