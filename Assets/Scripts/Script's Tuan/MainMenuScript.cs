using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject waitPanel;
    [SerializeField] private Button btnContinue;
    private bool isHasGame;
    private string pid;

    private async void Start()
    {
        waitPanel.SetActive(true);
        pid = await DataPersistence.instance.GetPid();
        isHasGame = await DataPersistence.instance.HasGameData(pid);
        waitPanel.SetActive(false);
        Debug.Log(isHasGame);
        if (isHasGame)
        {
            btnContinue.gameObject.SetActive(false);
        }
    }

    public void OnNewGameClicked()
    {
        DataPersistence.instance.NewGame();
        SceneManager.LoadScene(2);
    }

    public void OnLoadGameClicked()
    {
        DataPersistence.instance.Continue();
        SceneManager.LoadScene(3);
    }
}
