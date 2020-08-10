using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public HouseManager houseManager;
    public Item debugItem;

    public GridController currentGrid;

    private Pawn pawn;

    private void Awake()
    {
        pawn = GetComponentInChildren<Pawn>();
    }

    public void PlaceItem(Vector3 position, Item item)
    {
        if (currentGrid)
        {
            currentGrid.grid.PlaceItemFromPosition(position, item);
        }
    }

    private void Update()
    {
        float minDis = Mathf.Infinity;

        // Check for current floor
        for (int i = 0; i < houseManager.gridControllers.Length; i++)
        {
            float dis = Vector3.Distance(new Vector3(pawn.Position.x, houseManager.gridControllers[i].gridOrigin.position.y, pawn.Position.z), pawn.Position + Vector3.down);
            if(dis < minDis)
            {
                currentGrid = houseManager.gridControllers[i];
                minDis = dis;
            }
        }
    }
}
