using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private InventoryItem temp;

    public void OnDrop(PointerEventData eventData)
    {
        //there is no item in this slot
        if (transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
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
            else
            {
                //swap if not same kind or the same kind has total is bigger than InventoryManager.maxStackedItems
                temp = inventoryInSlot;
                Destroy(inventoryInSlot.gameObject);
                inventoryItemMouse.parentAfterDrag = transform;  //the item on the mouse given in the slot

                InventoryManager.instance.SwapItem(temp);
            }
        }
    }
}
