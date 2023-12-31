using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private GameObject cue;

    [SerializeField] private DialogueSO dialogue;

    [SerializeField] private List<RecipeSO> recipePapers;

    [SerializeField] private GameObject player;

    [SerializeField] private NPCController NPCController;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        cue.SetActive(false);

        NPCController = GetComponentInParent<NPCController>();
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
                    bool check = false;
                    for (int i = 0; i < RecipeManager.instance.listOfPaperUI.Count; i++)
                    {
                        if (recipePapers[0] == RecipeManager.instance.listOfPaperUI[i].recipeSO) //the player already had this recipe
                        {
                            check = true;
                            Debug.Log("loop: " + check);
                            //return;
                        }
                    }
                    if (check == false)
                    {
                        DialogueManager.instance.TakeRecipe(recipePapers[0]);
                        recipePapers[0] = null;
                    }
                    Debug.Log(check);
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

    public GameObject GetPlayer()
    {
        return player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;

            player = collision.gameObject;

            NPCController.enabled = true;
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
