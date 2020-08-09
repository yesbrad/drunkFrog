using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public float speed;

    Vector3 currentDirecrtion;

    public virtual void MoveDirection(Vector3 _direction)
    {
        currentDirecrtion = _direction;
    }
}
