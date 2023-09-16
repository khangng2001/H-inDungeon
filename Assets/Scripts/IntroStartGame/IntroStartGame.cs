using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroStartGame : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject canvas;
    [SerializeField] private bool isStart;
    [SerializeField] private float speed;
    [SerializeField] private string state = "non";

    [SerializeField] private GameObject panel;

    private void Awake()
    {
        camera = Camera.main;

        animator = GetComponentInChildren<Animator>();

        isStart = false;
    }

    private void Update()
    {
        if (isStart)
        {
            canvas.SetActive(false);

            if (camera.orthographicSize > 0.2f)
            {
                camera.orthographicSize -= 0.1f * speed * Time.deltaTime;

                if (camera.orthographicSize <= 1.5f)
                {
                    animator.Play("Open");
                }
            }
            else
            {
                if (state.Equals("NewGame"))
                {
                    DataPersistence.instance.NewGame();
                    SceneManager.LoadScene(6);
                }
                else if (state.Equals("ContinueGame"))
                {
                    DataPersistence.instance.Continue();
                    SceneManager.LoadScene(3);
                }
            }
        }
    }

    public void StartNewGame()
    {
        isStart = true;
        state = "NewGame";
    }

    public void StartContinueGame()
    {
        isStart = true;
        state = "ContinueGame";
    }

    public void LogOut()
    {
        // LOG OUT IN MONGO DB 

        //
        Application.Quit();
    }
}