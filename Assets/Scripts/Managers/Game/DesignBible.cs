using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DesignBible", menuName = "Design Bible")]
public class DesignBible : ScriptableObject
{
    public int ppScale = 200;
    public Color[] pencilColors;

    [Header("AI")]
    [Range(0,1)]
    public float chanceOfSocialising = 0.5f;
    [Range(0, 1)]
    public float chanceOfStartingConvesation = 0.7f;
    [Range(0, 1)]
    public float chanceOfUsingRandomItem = 0.5f;
}
