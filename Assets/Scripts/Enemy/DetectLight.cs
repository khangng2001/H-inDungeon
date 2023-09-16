using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectLight : MonoBehaviour
{
    List<Collider2D> colliders = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Light"))
        {
            GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;

            colliders.Add(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Light"))
        {
            GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Light"))
        {
            colliders.Remove(collision);

            if (colliders.Count == 0)
            {
                GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }
        }
    }
}
