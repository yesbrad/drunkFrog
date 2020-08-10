using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Static Prefabs")]
    public GameObject gridLine;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
