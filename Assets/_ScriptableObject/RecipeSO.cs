using UnityEngine;


[CreateAssetMenu(fileName = "Recipes", menuName = "ScriptableObjects/RecipeSO")]
public class RecipeSO : ScriptableObject
{
    [field: SerializeField]
    public Sprite RecipeImage { get; set; }

    [field: SerializeField]
    public string Title { get; set; }

    [field: SerializeField]
    [field: TextArea]
    public string Benefit { get; set; }

    [field: SerializeField]
    [field: TextArea]
    public string Ingredient { get; set; }
}
