using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject panelEndGame;
    private GameObject canvasEndGame;

    private void Awake()
    {
            
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvasEndGame = panelEndGame.transform.parent.gameObject;
            canvasEndGame.GetComponent<Canvas>().sortingOrder = 200;

            panelEndGame.GetComponent<Animator>().Play("Ending");
        }
    }
}
