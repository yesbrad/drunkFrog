using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DesignBible", menuName = "Design Bible")]
public class DesignBible : ScriptableObject
{
    public int ppScale = 200;
    public Color[] pencilColors;

    [Header("Seconds")]
    [Tooltip("How much time will a pencil be stuck before it decides to find a new task")]
    public float aiStuckBuffer = 5;
}
