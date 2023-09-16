using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoInventory : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ItemSO[] itemsToPickup;

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result == true)
        {
            Debug.Log("Item Added");
        }
        else
        {
            Debug.Log("Item Not Added");
        }
    }
}
