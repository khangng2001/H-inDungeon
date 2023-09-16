using UnityEngine;
using UnityEngine.UI;

public class CookBar : MonoBehaviour
{
    [SerializeField] private CookingManager cookingManager;
    [SerializeField] private Image barImage;

    private void Start()
    {
        cookingManager.OnProgressBarChanged += CookingManager_OnProgressBarChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void CookingManager_OnProgressBarChanged(object sender, CookingManager.OnProgressBarChangedEventArgs e)
    {
        barImage.fillAmount = e.progressBarNormalized;

        if (e.progressBarNormalized == 0f || e.progressBarNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
