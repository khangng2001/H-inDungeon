using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        
    }

    private void Start()
    {
        player = GetComponentInChildren<DialogueTrigger>().GetPlayer();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        SetOrderLayer();
    }

    private void SetOrderLayer()
    {
        Vector2 dir = player.transform.position - transform.position;
        if (dir.y > 0)
        {
            spriteRenderer.sortingOrder = 6;
        }
        else if (dir.y < 0)
        {
            spriteRenderer.sortingOrder = 4;
        }
    }
}
