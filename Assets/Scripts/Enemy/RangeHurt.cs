using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeHurt : MonoBehaviour
{
    [SerializeField] private bool isHurt;

    private bool wait;

    private void Awake()
    {
        isHurt = false;

        wait = false;
    }

    private void Update()
    {
        if (isHurt && !wait)
        {
            StartCoroutine(Wait());
            wait = true;
        }
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
        wait = false;
    }

    public bool GetIsHurt()
    {
        return isHurt;
    }

    public void SetIsHurt(bool enable)
    {
        isHurt = enable;
        wait = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sword")
        {
            isHurt = true;
        }
    }
}
