using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    private RangeOpen rangeOpen;

    private Animator animator;

    public bool PRESS_E = false;

    public bool PRESS_E_LAST = false;

    [SerializeField] private GameObject textEBefore;

    [SerializeField] private GameObject textEAfter;

    [SerializeField] private GameObject key;

    [SerializeField] private GameObject player;

    [SerializeField] private AudioClip chestOpen;
    private void Awake()
    {
        rangeOpen = GetComponentInChildren<RangeOpen>();

        animator = GetComponentInChildren<Animator>();
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

            if (!PRESS_E)
            {
                textEBefore.SetActive(true);
            }
            else if (PRESS_E && !PRESS_E_LAST)
            {
                textEAfter.SetActive(true);
            }
        }
        else
        {
            textEBefore.SetActive(false);
            textEAfter.SetActive(false);
        }
    }

    private void CheckClickOpen()
    {
        if (textEAfter.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E) && !PRESS_E_LAST)
            {
                PRESS_E_LAST = true;
                textEAfter.SetActive(false);
                // HANDLE KEY
                key.SetActive(false);

                player.transform.GetChild(3).gameObject.SetActive(true);
                //
            }
        }
        else if (textEBefore.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AudioManager.Instance.PlaySoundEffect(chestOpen);
                animator.Play("Open");
                PRESS_E = true;
                textEBefore.SetActive(false);
                key.GetComponentInChildren<Animator>().Play("Show");
            }
        }
    }
}
