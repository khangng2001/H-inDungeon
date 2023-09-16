using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private RangeOpen rangeOpen;
    private Animator animator;

    public bool PRESS_E = false;

    [SerializeField] private GameObject textE;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject colider;

    [SerializeField] private bool isOpened;

    private void Awake()
    {
        rangeOpen = GetComponentInChildren<RangeOpen>();

        animator = GetComponent<Animator>();

        isOpened = false;
    }

    private void Update()
    {
        CheckPlayerIn();

        CheckClickOpen();
    }

    private void CheckPlayerIn()
    {
        if (rangeOpen.GetIsIn())
        {
            player = rangeOpen.GetPlayer();

            if (player.transform.GetChild(3).gameObject.activeSelf == true && !isOpened)
            {
                textE.SetActive(true);

                //if (!PRESS_E)
                //{
                //    textE.SetActive(true);
                //}
            }
        }
        else
        {
            textE.SetActive(false);
        }
    }

    private void CheckClickOpen()
    {
        if (textE.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.transform.GetChild(3).gameObject.SetActive(false);

                animator.Play("Open");

                colider.SetActive(false);
                //GetComponent<Collider2D>().enabled = false;

                textE.SetActive(false);

                isOpened = true;
            }
        }
    }
}
