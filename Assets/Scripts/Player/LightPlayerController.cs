using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlayerController : MonoBehaviour
{
    PlayerInput input;

    private void Awake()
    {
        input = GetComponentInParent<PlayerInput>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        RotateByMouse();
    }

    private void FixedUpdate()
    {
        
    }

    void RotateByMouse()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    void RotateByKeyboard()
    {
        if (input.horizontal == 0 && input.vertical == 0)
        {
            return;
        }
        else if (input.horizontal == 0 && input.vertical > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (input.horizontal == 0 && input.vertical < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else if (input.horizontal < 0 && input.vertical == 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if (input.horizontal > 0 && input.vertical == 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
        else if (input.horizontal < 0 && input.vertical > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 45f);
        }
        else if (input.horizontal < 0 && input.vertical < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 135f);
        }
        else if (input.horizontal > 0 && input.vertical < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -135f);
        }
        else if (input.horizontal > 0 && input.vertical > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -45f);
        }
        else if (input.vertical == 0)
        {
            return;
        }
        else if (input.horizontal == 0)
        {
            return;
        }
        else if (input.vertical > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (input.vertical < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else if (input.horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if (input.horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
    }
}
