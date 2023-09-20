using Realms.Sync;
using Realms.Sync.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectMongoDb : MonoBehaviour
{
    public User user;
    private App app;

    [SerializeField] private GameObject loginUI;
    [SerializeField] private GameObject registerUI;
    [SerializeField] private GameObject loadingUI;
    [SerializeField] private GameObject notificationUI;

    [SerializeField] private TMP_InputField nameLoginField;
    [SerializeField] private TMP_InputField passLoginField;
    [SerializeField] private TMP_InputField nameRegisterField;
    [SerializeField] private TMP_InputField passRegisterField;
    [SerializeField] private TMP_InputField confirmRegisterField;
    [SerializeField] private TextMeshProUGUI textNotification;

    [SerializeField] private string appID = "hungryindungeon-wedpp";

    private enum SceneStatus
    {
        Login,
        Register,
        Loading,
        Notification
    }

    private SceneStatus currentStatus;

    //-------------------------------------------------------------------------------------------------------------------

    private void SwitchStateSceneStatus(SceneStatus newStatus, string message = "")
    {
        switch (currentStatus)
        {
            case SceneStatus.Notification:
                {
                    notificationUI.SetActive(false);
                    break;
                }
            default: { break; }
        }

        switch (newStatus)
        {
            case SceneStatus.Notification:
                {
                    textNotification.text = message;
                    notificationUI.SetActive(true);
                    break;
                }
            default: { break; }
        }
        currentStatus = newStatus;
        UpdateUI();
    }

    private void UpdateUI()
    {
        switch (currentStatus)
        {
            case SceneStatus.Login:
                {
                    loginUI.SetActive(true);
                    registerUI.SetActive(false);
                    loadingUI.SetActive(false);
                    notificationUI.SetActive(false);
                    break;
                }
            case SceneStatus.Register:
                {
                    loginUI.SetActive(false);
                    registerUI.SetActive(true);
                    loadingUI.SetActive(false);
                    notificationUI.SetActive(false);
                    break;
                }
            case SceneStatus.Loading:
                {
                    loginUI.SetActive(false);
                    registerUI.SetActive(false);
                    loadingUI.SetActive(true);
                    notificationUI.SetActive(false);
                    break;
                }
            case SceneStatus.Notification:
                {
                    break;
                }
        }
    }

    public async void OnSubmitRegister()
    {
        string name = nameRegisterField.text;
        string pass = passRegisterField.text;

        //if (pass != confirmRegisterField.text)
        //{
        //    SwitchStateSceneStatus(SceneStatus.Notification, "Pass doesn't match.");
        //}
        //else
        //{
        try
        {
            if (pass != confirmRegisterField.text)
            {
                throw new ArgumentException("Pass does not match.");
            }
            try
            {
                SwitchStateSceneStatus(SceneStatus.Loading);
                await app.EmailPasswordAuth.RegisterUserAsync(name, pass);
                SwitchStateSceneStatus(SceneStatus.Login);
            }
            catch (AppException ex)
            {
                SwitchStateSceneStatus(SceneStatus.Notification, ex.Message);
            }
        }
        catch (ArgumentException ex)
        {
            SwitchStateSceneStatus(SceneStatus.Notification, ex.Message);
        }
        //}
    }

    public async void OnSubmitLogin()
    {
        try
        {
            try
            {
                SwitchStateSceneStatus(SceneStatus.Loading);
                user = await app.LogInAsync(Credentials.EmailPassword(nameLoginField.text, passLoginField.text));
                SwitchStateSceneStatus(SceneStatus.Login);
                Debug.Log("User.Id: " + user.Id);
                SceneManager.LoadScene(1);
            }
            catch (AppException ex)
            {
                SwitchStateSceneStatus(SceneStatus.Notification, ex.Message);
            }
        }
        catch (ArgumentException ex)
        {
            SwitchStateSceneStatus(SceneStatus.Notification, ex.Message);
        }
    }

    public void OffNotification()
    {
        SwitchStateSceneStatus(SceneStatus.Login);
    }
    
    public void BackToLogin()
    {
        SwitchStateSceneStatus(SceneStatus.Login);
    }

    public void BackToRegister()
    {
        SwitchStateSceneStatus(SceneStatus.Register);
    }

    private void ConnectMongoDbHID()
    {
        app = App.Create(appID);
        SwitchStateSceneStatus(SceneStatus.Login);
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SwitchStateSceneStatus(SceneStatus.Loading);
        UpdateUI();
        ConnectMongoDbHID();
    }
}
