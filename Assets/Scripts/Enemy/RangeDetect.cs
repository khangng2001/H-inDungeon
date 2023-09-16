using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetect : MonoBehaviour
{
    [SerializeField] private bool isDetect;

    public GameObject player;

    private void Awake()
    {
        isDetect = false;
    }

    public bool GetIsDetect()
    {
        return isDetect;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject;

            isDetect = true;
        }
    }
}
