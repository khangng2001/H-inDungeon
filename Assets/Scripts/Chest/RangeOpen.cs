using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeOpen : MonoBehaviour
{
    [SerializeField] private bool isIn;

    [SerializeField] private GameObject player;

    private void Awake()
    {
        isIn = false;
    }

    public bool GetIsIn()
    {
        return isIn;
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isIn = true;

            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isIn = false;
        }
    }
}
