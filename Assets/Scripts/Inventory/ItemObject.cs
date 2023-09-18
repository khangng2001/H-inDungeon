using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemSO item;
    [SerializeField] private GameObject ui;

    private bool check;

    [Header("Sound")]
    [SerializeField] private AudioClip pickupItemSound;

    private void Awake()
    {
        ui.SetActive(false);
    }

    public ItemSO GetItem() 
    { 
        return item; 
    }

    private void Update()
    {
        if (check)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InventoryManager.instance.AddItem(item);
                Destroy(this.gameObject);
                AudioManager.Instance.PlaySoundEffect(pickupItemSound);
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
