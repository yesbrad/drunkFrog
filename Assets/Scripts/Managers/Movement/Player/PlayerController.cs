using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterPawn
{
    public PlayerManager Manager { get; private set; }
    public float playerDetectionDistance = 2;

    public Transform gridSelector;
    public Transform gridSelectorSingle;

    public Transform playerRotateContainer;
    private Vector3 inputDirection;

    private bool debugTime;

    public Vector3 oldPos;

    int hoverSize;

    public override void Init()
    {
        base.Init();
        Manager = GetComponentInParent<PlayerManager>();
        gridSelector.parent = Manager.transform;
        gridSelectorSingle.parent = Manager.transform;
        Manager.SetRotationContainer(playerRotateContainer);
	}

	private void Update () 
	{
        UpdateInput();

        Vector3 curAngle;

        if (Manager.CurrentGrid != null && Manager.InventoryManager.CurrentItem != null)
        {
            gridSelector.gameObject.SetActive(true);

            Vector2Int a = Manager.CurrentGrid.grid.GetGridPositionFromWorld(GetSelectionLocation());
            Vector2Int[] space = Manager.CurrentGrid.grid.GetGridSpace(a.x, a.y, Manager.InventoryManager.CurrentItem != null ? Manager.InventoryManager.CurrentItem.Data.size : 1, gridSelector);

            curAngle = Manager.InventoryManager.CurrentItem.Data.size == 1 ? Vector3.zero : PencilPartyUtils.RoundAnglesToNearest90(playerRotateContainer);

            gridSelector.position = Vector3.Lerp(gridSelector.position, GetSelectionLocation(), Time.deltaTime * 20);
            gridSelector.eulerAngles = curAngle;
            gridSelector.localScale = Vector3.Lerp(gridSelector.localScale, new Vector3(Manager.InventoryManager.CurrentItem.Data.size, 1, Manager.InventoryManager.CurrentItem.Data.size) * constants.GridCellSize, Time.deltaTime * 10);

            if (Manager.CurrentGrid.HasItem(GetSelectionLocation()))
            {
                Item item = Manager.CurrentGrid.GetItem(GetSelectionLocation());
                hoverSize = item.Data.size;
            }
            else
            {
                hoverSize = 1;
            }

        }
        else
        {
            gridSelector.gameObject.SetActive(false);
            curAngle = Vector3.zero;
        }

        gridSelectorSingle.localScale = Vector3.Lerp(gridSelectorSingle.localScale, new Vector3(hoverSize, 1, hoverSize) * constants.GridCellSize, Time.deltaTime * 10);
        gridSelectorSingle.position = Vector3.Lerp(gridSelectorSingle.position, GetSelectionLocation(), Time.deltaTime * 20);
        gridSelectorSingle.eulerAngles = curAngle ;

        UpdateSelectorColors();
    }

    private void UpdateInput ()
    {
        MoveDirection(inputDirection);
    }

    public void OnMove (InputAction.CallbackContext context)
    {
		Vector2 input = context.ReadValue<Vector2>();
        inputDirection.x = input.x;
        inputDirection.z = input.y;

        SetVelocity(input.sqrMagnitude);

        if(input != Vector2.zero)
            playerRotateContainer.localRotation = Quaternion.LookRotation(new Vector3(input.x, 0, input.y), Vector3.up);

    }

    private void UpdateSelectorColors()
    {
        if(Manager.CurrentGrid != null)
        {
            gridSelectorSingle.gameObject.SetActive(true);

            Vector2Int pos = Manager.CurrentGrid.grid.GetGridPositionFromWorld(GetSelectionLocation());

            if(Manager.InventoryManager.CurrentItem != null)
            {
                gridSelector.GetComponentInChildren<LineRenderer>().material.SetColor("_EmissionColor", Manager.CurrentGrid.grid.CanPlaceItemWithSize(pos.x, pos.y, Manager.InventoryManager.CurrentItem.Data.size, playerRotateContainer)
                    ? Color.blue
                    : Color.red);
            }


            gridSelectorSingle.GetComponentInChildren<LineRenderer>().material.SetColor("_EmissionColor", Manager.CurrentGrid.HasItem(GetSelectionLocation())
                ? Color.green
                : Color.white);

            gridSelectorSingle.GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", Manager.CurrentGrid.HasItem(GetSelectionLocation())
                ? Color.green
                : Color.white);
        }
        else
        {
            gridSelectorSingle.gameObject.SetActive(false);
        }
    }

    private Vector3 GetSelectionLocation ()
    {
        if (Manager.CurrentGrid == null)
            return Vector3.zero;

        return Manager.CurrentGrid.grid.GetWorldPositionFromWorld(transform.position + playerRotateContainer.forward * playerDetectionDistance);
    }

    
    private void OnDrawGizmos()
    {


        Gizmos.DrawCube(transform.position + playerRotateContainer.forward * playerDetectionDistance, Vector3.one);
        Gizmos.DrawCube(GetSelectionLocation(), Vector3.one);

        //return;
        if (Manager.CurrentGrid != null && Manager.InventoryManager.CurrentItem != null)
        {
            Vector2Int a = Manager.CurrentGrid.grid.GetGridPositionFromWorld(GetSelectionLocation());

            Vector2Int[] space = Manager.CurrentGrid.grid.GetGridSpace(a.x,a.y, Manager.InventoryManager.CurrentItem.Data.size, playerRotateContainer);

            Gizmos.color = Color.red;

            for (int i = 0; i < space.Length; i++)
            {
                Gizmos.DrawCube(Manager.CurrentGrid.grid.GetWorldGridCenterPositionFromWorld(Manager.CurrentGrid.grid.GetWorldPositionFromGrid(space[i].x, space[i].y)), Vector3.one);
            }
        }
    }
    

    public void OnPlaceItem (InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Manager.PlaceOrPickupCurrentItem(GetSelectionLocation());
        }
    }

    public void OnSwapItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Manager.InventoryManager.ShiftItems();
        }
    }

    public void OnDebugSpawn(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            for (int i = 0; i < 10; i++)
            {
                GameManager.instance.SpawnAI(Manager.HouseManager);
            }
        }
    }

    public void OnDebugTime(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            debugTime = !debugTime;
            Time.timeScale = debugTime ? 6 : 1;
        }
    }
}
