using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] private RangeDetect rangeDetect;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject player;

    [SerializeField] private bool isEnding;

    [SerializeField] private bool isSlinding;
    [SerializeField] private bool isBacking;
    [SerializeField] private bool isFinish;

    private void Awake()
    {
        rangeDetect = transform.parent.gameObject.GetComponentInChildren<RangeDetect>();
        camera = Camera.main.gameObject;
        isEnding = false;

        isSlinding = true;
        isBacking = false;
        isFinish = false;
    }

    private void Update()
    {
        if (isEnding)
        {
            player = rangeDetect.GetComponent<RangeDetect>().player;

            if (Vector2.Distance(camera.transform.position, transform.position) > 0.2f && isSlinding)
            {
                Lock();

                MoveToDragon();
            }
            else
            {
                isSlinding = false;

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

    public void SetIsEnding(bool enable)
    {
        isEnding = enable;
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
