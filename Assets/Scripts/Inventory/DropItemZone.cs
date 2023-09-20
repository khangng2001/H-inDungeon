using UnityEngine;
using UnityEngine.EventSystems;

public class DropItemZone : MonoBehaviour, IDropHandler
{
    public static DropItemZone instance;

    private void Awake()
    {
        instance = this;
        Hide();
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();  //get the item

        InventoryManager.instance.DropItemOnGround(inventoryItem);
        InventoryManager.instance.DropItem(inventoryItem);

        Hide();
    }
}
