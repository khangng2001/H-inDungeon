using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/DialogueSO")]
public class DialogueSO : ScriptableObject
{
    [Header("Type")]
    public DialogueType type;

    public string name;
    public Sprite face;
    public string[] sentences;
}

public enum DialogueType
{
    Basic,
    Choise
}
