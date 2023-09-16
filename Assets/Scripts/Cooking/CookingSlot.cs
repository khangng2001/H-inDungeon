using UnityEngine;
using UnityEngine.EventSystems;

public class CookingSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (inventoryItem.item.type == ItemType.Ingredient)
        {
            //there is no item in this slot
            if (transform.childCount == 0)
            {
                inventoryItem.parentAfterDrag = transform;
            }

            //this slot has item
            if (transform.childCount > 0)
            {
                InventoryItem inventoryItemMouse = eventData.pointerDrag.GetComponent<InventoryItem>();  //the item on the mouse
                InventoryItem inventoryInSlot = this.GetComponentInChildren<InventoryItem>(); //the item in slot

                //add if they are same kind and total amount is smaller or equal than InventoryManager.maxStackedItems
                int totalCount = inventoryItemMouse.count + inventoryInSlot.count;
                if (inventoryItemMouse.item.name == inventoryInSlot.item.name && (totalCount <= InventoryManager.instance.tempMaxStackedItems))
                {
                    inventoryInSlot.count = totalCount;
                    inventoryInSlot.RefreshCount();
                    Destroy(inventoryItemMouse.gameObject);
                }
            }
        }
        else
        {
            Debug.Log("This is not an ingredient");
        }
    }
}