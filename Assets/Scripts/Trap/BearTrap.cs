using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Leg"))
        {
            animator.Play("ActiveBearTrap");
            StartCoroutine(AfterActive());
        }
    }

    IEnumerator AfterActive()
    {
        yield return new WaitForSeconds(1f);
        Destroy(transform.parent.gameObject);
    }
}
