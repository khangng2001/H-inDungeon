using Realms.Sync;
using Realms.Sync.Exceptions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistence : MonoBehaviour
{
    MongoClient.Collection<GameData> collection;

    private string pid;
    public bool isNewGame;

    private CloudDataHandler dataHandler;
    private List<IDataPersistence> dataPersistencesObjects;

    public static DataPersistence instance { get; private set; }

    //----------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        try
        {
            if (instance != null)
            {
                Debug.LogError("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
                Destroy(this.gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            isNewGame = false;
        }
        catch (AppException ex)
        {
            Debug.LogException(ex);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnLoaded;
    }

    public async void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        pid = await GetPid();
        Debug.Log("pid: " + pid);

        this.dataHandler = new CloudDataHandler(collection);

        dataPersistencesObjects = FindAllDataPersistenceObjects();
    }

    public void OnSceneUnLoaded(Scene scene)
    {
        Debug.Log("OnSceneUnLoaded Called: " + scene.name);
        //SaveGame();
    }

    private async Task<string> FindPlayerPid(string findPid)
    {
        try
        {
            GameData myAccount = await collection.FindOneAsync(new { pid = findPid });
            return myAccount.Pid;
        }
        catch (AppException ex)
        {
            Debug.LogException(ex);
            return null;
        }
    }

    public void NewGame()
    {
        isNewGame = true;
        //GameData myAccount = await collection.FindOneAsync(new { pid = pid });
        //// Create new data for game
        //myAccount = new GameData();
    }

    public async void Continue()
    {
        GameData myAccount = await collection.FindOneAsync(new { pid = pid });
        SceneManager.LoadScene(myAccount.Scene);
        LoadGame();
    }

    public async void LoadGame()
    {
        try
        {
            GameData myAccount = await collection.FindOneAsync(new { pid = pid });

            // Load any saved data from a player in mongodb
            Debug.Log("pid error: " + myAccount.Pid);
            myAccount = await dataHandler.Load(myAccount.Pid);

            // Push the load data to all other script that need it
            foreach (IDataPersistence dataPersistenceObj in dataPersistencesObjects)
            {
                dataPersistenceObj.LoadData(myAccount);
            }
        }
        catch (AppException ex)
        {
            Debug.LogException(ex);
        }
    }

    public async void SaveGame()
    {
        GameData myAccount = await collection.FindOneAsync(new { pid = pid });

        // Pass the data other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.SaveData(ref myAccount);
        }

        // Save that data to a player in mongodb  
        dataHandler.Save(myAccount, myAccount.Pid);
        Debug.Log("Save success");
    }

    public async Task<string> GetPid()
    {
        User user = GameObject.FindObjectOfType<ConnectMongoDb>().GetComponent<ConnectMongoDb>().user;
        var mongoDbClient = user.GetMongoClient("mongodb-atlas");
        var database = mongoDbClient.GetDatabase("HungryInDungeon");
        collection = database.GetCollection<GameData>("Player");

        pid = await FindPlayerPid(user.Id);

        return pid;
    }

    public async Task<bool> HasGameData(string pid)
    {
        GameData myAccount = await collection.FindOneAsync(new { pid = pid });
        return myAccount.Scene == 0;
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
