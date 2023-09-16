using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithMouse : MonoBehaviour
{
    [SerializeField] private float speedRotate = 5f;

    void Update()
    {

        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.up = Vector3.Lerp(transform.up, new Vector3(direction.x, direction.y, 0), Time.deltaTime * speedRotate);
    }
}
