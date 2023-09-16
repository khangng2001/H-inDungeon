using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegEnemy : MonoBehaviour
{
    public bool fall = false;
    [SerializeField] private GameObject enemy;

    private void Update()
    {
        if (fall)
        {
            enemy.GetComponent<EnemyController>().enabled = false;


            if (enemy.transform.localScale.x == 0f || enemy.transform.localScale.y <= 0f)
            {
                Destroy(gameObject);
            }
            else if (enemy.transform.localScale.x < 0f)
            {
                enemy.transform.localScale -= new Vector3(-0.1f, 0.1f, 0f);
            }
            else
            {
                enemy.transform.localScale -= new Vector3(0.1f, 0.1f, 0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hole"))
        {
            fall = true;
        }
    }
}
