using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    [SerializeField] private Transform pointFall;
    [SerializeField] private Transform pointBoom;
    [SerializeField] private GameObject light2D;
    [SerializeField] private int numberOfUses = 1;
    [SerializeField] private float delay = 0.5f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float speed;

    private void Awake()
    {
        speed = 5f;

        isBoom = false;
        isAfterBoom = false;

        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }

    private void Start()
    {
        transform.position = pointFall.position;
    }

    private void Update()
    {
        if (numberOfUses > 0)
        {
            if (checkDistance())
            {
                if (!isBoom)
                {
                    animator.Play("Boom");

                    isBoom = true;

                    StartCoroutine(Boom());
                }
            }
            else
            {
                Falling();
            }
        }
    }

    private void Falling()
    {
        transform.position = Vector2.MoveTowards(transform.position ,pointBoom.position, speed * Time.deltaTime);
    }

    private bool checkDistance()
    {
        if (Vector2.Distance(transform.position, pointBoom.position) <= 0.2f)
        {
            return true;
        }

        return false;
    }

    private bool isBoom = false;
    IEnumerator Boom()
    {
        yield return new WaitForSeconds(0.25f);

        spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        animator.Play("Hide");

        light2D.SetActive(false);

        numberOfUses -= 1;

        if (numberOfUses > 0)
        {
            StartCoroutine(AfterBoom());
        }
    }

    private bool isAfterBoom = false;
    IEnumerator AfterBoom()
    {
        yield return new WaitForSeconds(delay);

        transform.position = pointFall.position;

        spriteRenderer.maskInteraction = SpriteMaskInteraction.None;

        animator.Play("Fall");

        isBoom = false;

        light2D.SetActive(true);

    }
}
