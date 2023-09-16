using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class ChangeToNewScene : MonoBehaviour
{
    [FormerlySerializedAs("goToSceneName")]
    [Header("Scene's Name To Go")]
    [SerializeField] private string sceneName;

    private bool check;

    private void Update()
    {
        if (check)
        {
            //Player reaches the door
            GameManager.instance.ProceedScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            check = true;
        }
        else
        {
            check = false;
        }
    }
}
