using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    private const float BoundX = 1.5f;
    private const float BoundY = 1f;

    private float deltaX = 0f;
    private float deltaY = 0f;
    private Vector3 movingDir = Vector3.zero;
    
    void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        if(target == null) return;
    }

    private void LateUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        //Create the vector value used for moving the camera
        Vector3 movingDirection = Vector3.zero;

        deltaX = target.position.x - transform.position.x;
        deltaY = target.position.y - transform.position.y;
        //Debug.Log("Before: "+ deltaX);
        if (deltaX > BoundX || deltaX < -BoundX)
        {
            if (target.position.x > transform.position.x)
            {
                movingDirection.x = deltaX - BoundX;
            }
            else
            {
                movingDirection.x = deltaX + BoundX;

            }
        }

        if (deltaY > BoundY || deltaY < -BoundY)
        {
            if (target.position.y > transform.position.y)
            {
                movingDirection.y = deltaY - BoundY;
            }
            else
            {
                movingDirection.y = deltaY + BoundY;
            }
        }

        transform.position += new Vector3(movingDirection.x, movingDirection.y, 0f);
    }
}
