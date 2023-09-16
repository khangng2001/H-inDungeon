using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private RangeDetect rangeDetect;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject player;

    [SerializeField] private bool isSlinding;
    [SerializeField] private bool isBacking;
    [SerializeField] private bool isFinish;
    [SerializeField] private bool canMove;

    private void Awake()
    {
        rangeDetect = transform.parent.gameObject.GetComponentInChildren<RangeDetect>();
        camera = Camera.main.gameObject;

        isSlinding = true;
        isBacking = false;
        isFinish = false;
        canMove = false;
    }

    private void Update()
    {
        // WHEN PLAYER COMING DRAGON HOME
        if (rangeDetect.GetIsDetect())
        {
            // GET OBJECT PLAYER
            player = rangeDetect.GetComponent<RangeDetect>().player;

            // WHEN CAMERA AND DRAGON ENOUGH CLOSE
            if (Vector2.Distance(camera.transform.position, transform.position) > 0.2f && isSlinding)
            {
                // LOCK PLAYER AND CAMERA
                Lock();

                // CAN MOVE AFTER START DRAGON'S ROAR SOUND
                if (canMove)
                {
                    MoveToDragon();
                }

                // SET UP TIME TO HEAR DRAGON'S ROAR SOUND
                if (!isStartIntroSound)
                {
                    isStartIntroSound = true;

                    StartCoroutine(StartIntroSound());
                }
            }
            else
            {
                isSlinding = false;

                // WHEN CAMERA AND PLAYER ENOUGH CLOSE
                if (Vector2.Distance(player.transform.position, camera.transform.position) > 0.2f && isBacking)
                {
                    MoveToPlayer();
                }
                else if (isBacking)
                {
                    UnLock();

                    isBacking = false;
                }

            }
        }
    }

    private bool isStartIntroSound = false;
    IEnumerator StartIntroSound()
    {
        yield return new WaitForSeconds(2f);

        canMove = true;
    }


    public void SetIsBacking(bool enable)
    {
        isBacking = enable;
    }

    public bool GetIsFinish()
    {
        return isFinish;
    }

    public bool GetIsSliding()
    {
        return isSlinding;
    }

    private void MoveToDragon()
    {
        Vector2 dir = transform.position - camera.transform.position;
        dir.Normalize();
        camera.transform.Translate(dir * Time.deltaTime * 5f);
    }

    private void MoveToPlayer()
    {
        Vector2 dir = player.transform.position - camera.transform.position;
        dir.Normalize();
        camera.transform.Translate(dir * Time.deltaTime * 5f);
    }

    private void Lock()
    {
        camera.GetComponent<CameraController>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<PlayerInput>().enabled = false;
    }

    private void UnLock()
    {
        camera.GetComponent<CameraController>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<PlayerInput>().enabled = true;
        isFinish = true;
    }
}
