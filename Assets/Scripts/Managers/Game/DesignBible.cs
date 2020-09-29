using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DesignBible", menuName = "Design Bible")]
public class DesignBible : ScriptableObject
{
    [Header("Difficulty")]
    [Range(200, 10000)]
    public int ppScale = 200;
    [Tooltip("How much of out HInv stats will be added in to the PP Calculation. 1 = All of it!")]
    [Range(1, 100)]
    public int ppInventoryScale = 10;

    [Header("Cosmetics")]
    public Color[] pencilColors;

    [Header("Seconds")]
    [Tooltip("How much time will a pencil be stuck before it decides to find a new task")]
    public float aiStuckBuffer = 5;
}
