using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipePaperUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image recipePaperImage;
    [SerializeField] private GameObject selected;

    public RecipeSO recipeSO;

    public event Action<RecipePaperUI> OnPaperClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPaperClicked?.Invoke(this);
    }

    public void SetData(Sprite sprite)
    {
        this.recipePaperImage.gameObject.SetActive(true);
        this.recipePaperImage.sprite = sprite;
    }

    public void Select()
    {
        selected.SetActive(true);
    }

    public void Deselect()
    {
        selected.SetActive(false);
    }
}
