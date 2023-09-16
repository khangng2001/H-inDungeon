using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private GameObject cue;

    [SerializeField] private DialogueSO dialogue;

    [SerializeField] private List<RecipeSO> recipePapers;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        cue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange)
        {
            cue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && !DialogueManager.instance.isDialoguePlaying)
            {
                if (recipePapers[0] != null)
                {
                    DialogueManager.instance.TakeRecipe(recipePapers[0]);
                    recipePapers[0] = null;
                }
                DialogueManager.instance.EnterDialogueMode(dialogue);
            }
        }
        else
        {
            cue.SetActive(false);
        }
    }

 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
