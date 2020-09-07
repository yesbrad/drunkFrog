using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public enum PawnState
    {
        Free,
        Talking,
        Dancing
    }

    public float speed;
    public Animator animator;
    Vector3 currentDirecrtion;
    public SkinnedMeshRenderer pencilRenderer;
    public Vector3 Position { get { return transform.position; } }

    public void Awake()
    {
        Init(); 
    }

    public virtual void Init()
    {
        if (pencilRenderer != null)
        {
            pencilRenderer.materials[0].SetColor("_Color", GameManager.instance.DesignBible.pencilColors[Random.Range(0, GameManager.instance.DesignBible.pencilColors.Length)]);
        }
    }

    public virtual void MoveDirection(Vector3 _direction)
    {
        currentDirecrtion = _direction;
    }

    public void SetVelocity(float val)
    {
        if (animator != null)
            animator.SetFloat("velocity", val);
        else
            Debug.LogError("Missing Animator on Pawn", gameObject);
    }

    public void SetPosition (Vector3 position)
    {
        Debug.Log("Setting POsition Pawn" + position);
        transform.position = position;
    }

    public void SetState(PawnState state)
    {
        animator.SetBool("isFree", state == PawnState.Free);
        animator.SetBool("isTalking", state == PawnState.Talking);
        animator.SetBool("isDancing", state == PawnState.Dancing);
    }
}
