using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IDataPersistence
{
    public static GameManager instance;
    [Header("Inventory")]
    public List<ItemSO> item;   //item.length = inventorySlot.length = 7
    public List<int> count;
    [Header("Recipe")]
    public List<RecipeSO> recipes;
    
    private static int sceneIndex = 2;
    [SerializeField] public Vector3[] entrances;
    [SerializeField] private int currentIndexEntrance = 0;

    [SerializeField] private GameObject blackPanel;
    [SerializeField] private GameObject blackFade;
    [SerializeField] private GameObject panelPause;

    public int CurrentIndexEntrance
    {
        get
        {
            return currentIndexEntrance;
        }
        private set
        {
            
        }
    }

    private void Awake()
    {
        blackPanel.SetActive(true);
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        if (DataPersistence.instance.isNewGame)
        {
            blackPanel.SetActive(false);
            blackFade.GetComponent<Animator>().Play("FadeIn");
        }
    }

    private void Update()
    {
    }

    private void OnEnable()
    {
        Debug.Log("currentIndex: " + currentIndexEntrance);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBackgroundMusic();

        this.gameObject.SetActive(true);
    }


    private void PlayBackgroundMusic()
    {
        if (SceneManager.GetActiveScene().name == "Scene_01")
        {
            AudioManager.Instance.PlayBackgroundMusic();
            AudioManager.Instance.StopDungeonMusic();
        }
        else
        {
            AudioManager.Instance.StopBackgroundMusic();
            AudioManager.Instance.PlayDungeonMusic();
        }
    }
    public void SaveDataInventory()
    {
        for (int i = 0; i < item.Count; i++)
        {
            item[i] = InventoryManager.instance.SaveDataItem(i);
            count[i] = InventoryManager.instance.SaveDataCount(i);
        }
    }
    public void LoadDataInventory()
    {
        for (int i = 0; i < item.Count; i++)
        {
            if (item[i] != null)
            {
                InventoryManager.instance.LoadData(i, item[i], count[i]);
            }
        }
    }
    public void SaveDataRecipe(RecipeSO recipeSO)
    {
        recipes.Add(recipeSO);
    }
    public void LoadDataRecipe()
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            if (recipes[i] != null)
            {
                RecipeManager.instance.AddRecipe(recipes[i]);
            }
        }
    }

    //CHANGE SCENE
    public void ProceedScene()
    {
        StartCoroutine(ProceedWaitForBlackCurtain());
    }
    
    
    
    public void GoPreviousScene()
    {
        StartCoroutine(PreviousWaitForBlackCurtain());
    }
    
    private IEnumerator ProceedWaitForBlackCurtain()
    {
        sceneIndex++;
        currentIndexEntrance++;

        blackFade.transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = 200;
        blackFade.GetComponent<Animator>().Play("FadeOut");
        yield return new WaitForSeconds(1f);
        PlayerController.instance.transform.position = entrances[currentIndexEntrance];
        
        SceneManager.LoadScene(sceneIndex);
        yield return new WaitForSeconds(0.5f);
        blackFade.transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = 0;
        blackFade.GetComponent<Animator>().Play("FadeIn");
        
    }

    private IEnumerator PreviousWaitForBlackCurtain()
    {
        currentIndexEntrance--;
        sceneIndex--;
        blackFade.transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = 200;
        blackFade.GetComponent<Animator>().Play("FadeOut");
        yield return new WaitForSeconds(1f);
        PlayerController.instance.transform.position = entrances[currentIndexEntrance + 4];
        SceneManager.LoadScene(sceneIndex);
        yield return new WaitForSeconds(0.5f);
        blackFade.transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = 0;
        blackFade.GetComponent<Animator>().Play("FadeIn");
        
    }

    public IEnumerator WaitOnLoad() // DUOC GOI TU PORTAL LEVEL TUTORIAL
    {
        blackFade.GetComponent<Animator>().Play("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
        PlayerController.instance.transform.position = new Vector3(0f, 0f, 0f);

        yield return new WaitForSeconds(0.5f);
        blackFade.GetComponent<Animator>().Play("FadeIn");
    }

    public int GetSceneIndex()
    {
        return sceneIndex;
    }
    
    //=================SAVE AND LOAD DATA================
    public void LoadData(GameData data)
    {
        sceneIndex = data.Scene;
        currentIndexEntrance = sceneIndex - 2;
        blackPanel.SetActive(false);
        blackFade.GetComponent<Animator>().Play("FadeIn");
    }

    public void SaveData(ref GameData data)
    {
        data.Scene = sceneIndex;
    }

    //
    public void PauseGame()
    {
        Time.timeScale = 0;
        panelPause.SetActive(true);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        panelPause.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
