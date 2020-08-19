using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kit", menuName = "Build Kit")]
public class BuildKit : ScriptableObject
{
    public List<GameObject> walls;
    public List<GameObject> foundations;
    public List<GameObject> floors;
    public List<GameObject> stairs;
}
