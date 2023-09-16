using System;
using UnityEngine;

namespace Portals
{
    public class ExitDoor : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.instance.GoPreviousScene();
            }
        }
    }
}
