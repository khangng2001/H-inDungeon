using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDescriptionUI : MonoBehaviour
{
    [SerializeField] private Image recipeImage;
    [SerializeField] private TMP_Text recipeTitle;
    [SerializeField] private TMP_Text recipeBenefit;
    [SerializeField] private TMP_Text recipeIngredient;

    private void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        this.recipeImage.gameObject.SetActive(false);
        this.recipeTitle.text = "";
        this.recipeBenefit.text = "";
        this.recipeIngredient.text = "";
    }

    public void SetDescription(Sprite sprite, string rTitle, string rBenefit, string rIngredient)
    {
        this.recipeImage.gameObject.SetActive(true);
        this.recipeImage.sprite = sprite;
        this.recipeTitle.text = rTitle;
        this.recipeBenefit.text = rBenefit;
        this.recipeIngredient.text = rIngredient;
    }

}
