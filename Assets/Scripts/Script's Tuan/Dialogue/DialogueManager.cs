using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Panel")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI dialogueName;
    [SerializeField] private Image face;
    [SerializeField] private Button btncontinue;

    [Header("In game")]
    [SerializeField] private GameObject notification;
    [SerializeField] private GameObject choise;

    [Header("In Script")]
    private string dialogueType;
    private string[] sentences;
    private float wordSpeed;
    private int index;
    private RecipeSO recipePapers;

    public bool isDialoguePlaying { get; private set; }

    public static DialogueManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found a instance in scene. Remove this object");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
       //DontDestroyOnLoad(this.gameObject); 
    }

    private void Start()
    {
        index = 0;
        isDialoguePlaying = false;
        dialoguePanel.SetActive(false);
        choise.SetActive(false);
        notification.SetActive(false);
        btncontinue.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isDialoguePlaying)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueText.text == sentences[index])
            {
                wordSpeed = 0.01f;
                ContinueStory();
            }
            else
            {
                wordSpeed = 0.001f;
            }
        }

        //if (index == sentences.Length - 1 && dialogueType == "Choise")
        //{
        //    choise.SetActive(true);
        //    isDialoguePlaying = false;
        //}
    }
    public void TakeRecipe(RecipeSO recipeSOs)
    {
        recipePapers = recipeSOs;
    }

    public void EnterDialogueMode(DialogueSO dialogue)
    {
        Debug.Log("cos chay ");

        isDialoguePlaying = true;
        dialoguePanel.SetActive(true);
        sentences = dialogue.sentences;
        dialogueName.text = dialogue.name;
        face.sprite = dialogue.face;
        dialogueType = dialogue.type.ToString();

        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        btncontinue.gameObject.SetActive(false);
        dialogueText.text = "";
        foreach (char letter in sentences[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        btncontinue.gameObject.SetActive(true);
    }

    private void ExitDialogueMode()
    {
        isDialoguePlaying = false;
        dialoguePanel.SetActive(false);
        choise.SetActive(false);
        dialogueText.text = "";
        index = 0;
        wordSpeed = 0.1f;
        AddRecipe(recipePapers);
    }

    private void ContinueStory()
    {
        if (index < sentences.Length - 1)
        {
            if (index == sentences.Length - 2 && dialogueType == "Choise")
            {
                choise.SetActive(true);
            }
            index++;
            StartCoroutine(Typing());
        }
        else if (index == sentences.Length - 1)
        {
            ExitDialogueMode();
        } 
    }

    public void AddRecipe(RecipeSO recipeSO)
    {
        if (recipePapers != null)
        {
            RecipeManager.instance.AddRecipe(recipePapers);
            GameManager.instance.SaveDataRecipe(recipePapers);
            recipePapers = null;
            notification.SetActive(true);
        }
    }

    public void OnClickChoise0()
    {
        notification.SetActive(true);
        ExitDialogueMode();
    }

    public void OnClickChoise1()
    {
        ExitDialogueMode();
    }

    public void OnOKButton()
    {
        notification.SetActive(false);
    }
}
