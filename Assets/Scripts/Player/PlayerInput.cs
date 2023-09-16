using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float horizontal;
    public float vertical;

    public Vector2 inputMosue;

    public bool inputInteract;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        inputMosue = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
