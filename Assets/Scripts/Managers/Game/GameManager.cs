using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Grid grid = new Grid(100, 150, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
