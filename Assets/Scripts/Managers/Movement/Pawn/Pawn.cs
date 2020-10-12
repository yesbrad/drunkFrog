using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour, IDetection
{
    public enum PawnState
    {
        Free,
        Talking,
        Dancing,
        KnockedOut,
        Attacking
    }

    [SerializeField]
    protected Transform pawnTimelineContainer;

    public Transform rotateContainer;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private SkinnedMeshRenderer pencilRenderer;
  
    private Vector3 currentDirecrtion;

    private ItemCharacterSlot currentSlot;
    
    public Vector3 Position { get { return transform.position; } }
    public Animator PawnAnimator { get { return animator; } }

    private Transform originalParent;

    public bool Occupied { get; private set; }

    public event Action<PawnState> OnUpdatePawnState;
    private PawnState state;

    public CharacterManager CharacterManager { get; private set; }

    public virtual void Init(CharacterManager manager)
    {
        CharacterManager = manager;

        if (pencilRenderer != null)
        {
            pencilRenderer.materials[0].SetColor("_Color", GameManager.instance.DesignBible.pencilColors[UnityEngine.Random.Range(0, GameManager.instance.DesignBible.pencilColors.Length)]);
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
        transform.position = position;
    }

    public void StartTimline(ItemCharacterSlot slot)
    {
        currentSlot = slot;
        slot.Take();

        originalParent = pawnTimelineContainer.parent;
        pawnTimelineContainer.parent = slot.transform;
        ResetLocalTransform();
    }

    public void EndTimeline()
    {
        if(originalParent != null)
            pawnTimelineContainer.parent = originalParent;
        
        originalParent = null;
        ResetLocalTransform();
        currentSlot.Release();
    }

    private void ResetLocalTransform()
    {
        pawnTimelineContainer.localPosition = Vector3.zero;
        pawnTimelineContainer.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public virtual void SetRotation(Vector3 rotation)
    {
        SetRotation(Quaternion.Euler(rotation));
    }

    public virtual void SetRotation(Quaternion rotation)
    {
        rotateContainer.rotation = rotation;
    }

    public void SetState(PawnState state)
    {
        this.state = state;
        OnUpdatePawnState?.Invoke(state);

        animator.SetBool("isFree", state == PawnState.Free || state == PawnState.KnockedOut);
        animator.SetBool("isTalking", state == PawnState.Talking);
        animator.SetBool("isDancing", state == PawnState.Dancing);
    }

    public void LockPawn (bool locked)
    {
        Occupied = locked;
    }

    public void Select()
    {
        //throw new NotImplementedException();
    }

    public void Deselect()
    {
        //throw new NotImplementedException();
    }

    public void Attack()
    {
        SetState(PawnState.Attacking);
        animator.SetTrigger("Attack");
        LockPawn(true);
        StartCoroutine(UnlockAttack());
    }

    IEnumerator UnlockAttack()
    {
        yield return new WaitForSeconds(1);
        LockPawn(false);
        SetState(PawnState.Free);
    }

    public void KnockOut()
    {
        SetState(PawnState.KnockedOut);
        animator.SetTrigger("KnockOut");
        LockPawn(true);
        StartCoroutine(UnlockKnockout());
    }

    IEnumerator UnlockKnockout()
    {
        yield return new WaitForSeconds(12);
        LockPawn(false);
        SetState(PawnState.Free);
    }

    public void StartInteract(CharacterManager manager, Action onFinishInteraction)
    {
        if (!Occupied)
        {
            manager.Pawn.Attack();
            KnockOut();
        }
    }


}
