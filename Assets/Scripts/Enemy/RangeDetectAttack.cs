using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetectAttack : MonoBehaviour
{
    [SerializeField] private bool isDetectAttack;

    private void Awake()
    {
        isDetectAttack = false;
    }

    public bool GetIsDetectAttack()
    {
        return isDetectAttack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isDetectAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isDetectAttack = false;
        }
    }
}
