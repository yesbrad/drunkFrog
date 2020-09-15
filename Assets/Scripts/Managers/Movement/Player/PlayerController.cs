using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Pawn
{
    public PlayerManager Manager { get; private set; }
    public float playerDetectionDistance = 2;

    [SerializeField]
    private SelectorController selectorController;

    public Transform gridSelector;
    public Transform gridSelectorSingle;

    public Transform playerRotateContainer;
    private Vector3 inputDirection;

    private bool debugTime;

    public Vector3 oldPos;

    ItemSize hoverSize = new ItemSize();

    internal bool Locked;

    private Vector3 movePosition;
    private CharacterController controller;

    public override void Init()
    {
        base.Init();
        controller = GetComponent<CharacterController>();
        Manager = GetComponentInParent<PlayerManager>();
        gridSelector.parent = Manager.transform;
        gridSelectorSingle.parent = Manager.transform;
        Manager.SetRotationContainer(playerRotateContainer);
	}

	private void Update ()
    {
        MoveDirection(inputDirection);
        UpdateSelectorPosition();
        UpdateSelectorColors();
    }

    private void UpdateSelectorPosition()
    {
        Vector3 curAngle;

        if (Manager.CurrentGrid != null && Manager.InventoryManager.CurrentItem != null)
        {
            gridSelector.gameObject.SetActive(true);

            curAngle = Manager.InventoryManager.CurrentItem.Data.size.IsSingle() ? Vector3.zero : PencilPartyUtils.RoundAnglesToNearest90(playerRotateContainer);

            gridSelector.position = Vector3.Lerp(gridSelector.position, GetSelectionCenterLocation(), Time.deltaTime * 20);
            gridSelector.eulerAngles = curAngle;
            //gridSelector.localScale = Vector3.Lerp(gridSelector.localScale, new Vector3(Manager.InventoryManager.CurrentItem.Data.size.x, 1, Manager.InventoryManager.CurrentItem.Data.size.y) * constants.GridCellSize, Time.deltaTime * 10);

            selectorController?.UpdateLine(Manager.InventoryManager.CurrentItem.Data.size);


            if (Manager.CurrentGrid.HasItem(GetSelectionLocation()))
            {
                Item item = Manager.CurrentGrid.GetItem(GetSelectionLocation());
                hoverSize.x = item.Data.size.x;
                hoverSize.y = item.Data.size.y;
            }
            else
            {
                hoverSize.x = 1;
                hoverSize.y = 1;
            }

        }
        else
        {
            gridSelector.gameObject.SetActive(false);
            curAngle = Vector3.zero;
        }

        gridSelectorSingle.position = Vector3.Lerp(gridSelectorSingle.position, GetSelectionCenterLocation(), Time.deltaTime * 20);
        gridSelectorSingle.eulerAngles = curAngle;
    }

    public void Spawn (Vector3 position)
    {
        StartCoroutine(DelayedSpawn(position));
    }

    IEnumerator DelayedSpawn (Vector3 position)
    {
        yield return new WaitForSeconds(0.01f);
        transform.position = position;
    }

    public void OnMove (InputAction.CallbackContext context)
    {
        if (Occupied)
            return;

		Vector2 input = context.ReadValue<Vector2>();
        inputDirection.x = input.x;
        inputDirection.z = input.y;

        SetVelocity(input.sqrMagnitude);

        if(input != Vector2.zero)
            playerRotateContainer.localRotation = Quaternion.LookRotation(new Vector3(input.x, 0, input.y), Vector3.up);

    }

    public override void MoveDirection(Vector3 _direction)
    {
        if (Occupied)
            return;

        movePosition = _direction * speed * Time.deltaTime;
        controller.SimpleMove(movePosition);
    }

    public override void SetRotation(Vector3 rotation)
    {
        base.SetRotation(rotation);
        playerRotateContainer.rotation = Quaternion.Euler(rotation);
    }

    private void UpdateSelectorColors()
    {
        if(Manager.CurrentGrid != null)
        {
            gridSelectorSingle.gameObject.SetActive(true);

            GridPosition pos = Manager.CurrentGrid.grid.GetGridPositionFromWorld(GetSelectionLocation());

            if(Manager.InventoryManager.CurrentItem != null)
            {
                gridSelector.GetComponentInChildren<LineRenderer>().material.SetColor("_EmissionColor", Manager.CurrentGrid.grid.CanPlaceItemWithSize(pos.x, pos.y, Manager.InventoryManager.CurrentItem.Data.size, playerRotateContainer)
                    ? Color.blue
                    : Color.red);
            }


            gridSelectorSingle.GetComponentInChildren<LineRenderer>().material.SetColor("_EmissionColor", Manager.CurrentGrid.HasItem(GetSelectionLocation())
                ? Color.green
                : Color.white);
        }
        else
        {
            gridSelectorSingle.gameObject.SetActive(false);
        }
    }

    private Vector3 GetSelectionCenterLocation()
    {
        if (Manager == null || Manager.CurrentGrid == null)
            return Vector3.zero;

        return Manager.CurrentGrid.grid.GetWorldGridCenterPositionFromWorld(GetSelectionLocation());
    }

    private Vector3 GetSelectionLocation ()
    {
        if (Manager == null || Manager.CurrentGrid == null)
            return Vector3.zero;

        return Manager.CurrentGrid.grid.GetWorldPositionFromWorld(transform.position + (playerRotateContainer.forward * playerDetectionDistance));
    }

    /*
    private void OnDrawGizmos()
    {

        if (Manager == null || Manager.CurrentGrid != null && Manager.InventoryManager.CurrentItem != null)
        {
            Vector3 pos = GetSelectionLocation();

            Gizmos.DrawCube(transform.position + playerRotateContainer.forward * playerDetectionDistance, Vector3.one);

            Vector2Int a = Manager.CurrentGrid.grid.GetGridPositionFromWorld(pos);

            Vector2Int[] space = Manager.CurrentGrid.grid.GetGridSpace(a.x,a.y, Manager.InventoryManager.CurrentItem.Data.size, playerRotateContainer);

            Gizmos.color = Color.red;

            for (int i = 0; i < space.Length; i++)
            {
                Gizmos.DrawCube(Manager.CurrentGrid.grid.GetWorldPositionFromGrid(space[i].x, space[i].y), Vector3.one * 0.3f);
                Gizmos.DrawWireCube(Manager.CurrentGrid.grid.GetWorldGridCenterPositionFromWorld(Manager.CurrentGrid.grid.GetWorldPositionFromGrid(space[i].x, space[i].y)), Vector3.one * constants.GridCellSize);
            }
        }
    }
    */

    public void OnPlaceItem (InputAction.CallbackContext context)
    {
        if (Occupied)
            return;

        if (!Locked && context.performed)
        {
            Manager.PlaceOrPickupCurrentItem(GetSelectionLocation());
        }
    }

    public void OnSwapItem(InputAction.CallbackContext context)
    {
        if (Occupied)
            return;

        if (!Locked && context.performed)
        {
            //Manager.InventoryManager.ShiftItems();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (Occupied)
            return;

        if (!Locked && context.performed)
        {
            Manager.Interact(GetSelectionLocation());
        }
    }

    public void OnDebugSpawn(InputAction.CallbackContext context)
    {
        if (!Locked && context.performed)
        {
            for (int i = 0; i < 10; i++)
            {
                GameManager.instance.SpawnAI(Manager.HouseManager);
            }
        }
    }

    public void OnDebugTime(InputAction.CallbackContext context)
    {
        if (!Locked && context.performed)
        {
            debugTime = !debugTime;
            Time.timeScale = debugTime ? 6 : 1;
        }
    }
}
