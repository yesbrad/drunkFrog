using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private Item item;

    public void Init (Item newItem)
    {
        item = newItem;
    }
}
