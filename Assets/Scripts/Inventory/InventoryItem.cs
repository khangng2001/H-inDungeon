using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;
    public TextMeshProUGUI countText;

    [HideInInspector] public ItemSO item;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag; //to make sure the itemUI always on top

    public void SwapItem(ItemSO swapitem, int swapitemcount)
    {
        item = swapitem;
        image.sprite = swapitem.image;
        count = swapitemcount;
        RefreshCount();
    }

    public void InitialiseItem(ItemSO newitem)
    {
        item = newitem;
        image.sprite = newitem.image;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;                //hide count text if item's quantity = 1
        countText.gameObject.SetActive(textActive);
    }

    //Drag and Drop
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

        //Show DropItemUI
        DropItemZone.instance.Show();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);

        //Hide DropItemUI
        DropItemZone.instance.Hide();
    }
}
