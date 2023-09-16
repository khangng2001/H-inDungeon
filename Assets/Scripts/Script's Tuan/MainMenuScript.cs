using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
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
