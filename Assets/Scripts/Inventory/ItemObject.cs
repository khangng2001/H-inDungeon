using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemSO item;
    [SerializeField] private GameObject ui;

    private bool check;

    private void Awake()
    {
        ui.SetActive(false);
    }

    public ItemSO GetItem() 
    { 
        return item; 
    }

    public ItemObject GetItemObject()
    {
        return this;
    }

    private void Update()
    {
        if (check)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InventoryManager.instance.AddItem(item);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ui.SetActive(true);
            check = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ui.SetActive(false);
            check = false;
        }
    }
}
