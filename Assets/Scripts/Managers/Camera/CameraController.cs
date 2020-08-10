using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float speed = 10;
    private Transform myTransform;

    void Start()
    {
        myTransform = transform;
    }

    void Update()
    {
        if(target)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, target.position, speed * Time.deltaTime);
        }
    }
}
