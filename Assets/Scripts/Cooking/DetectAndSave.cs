using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAndSave : MonoBehaviour
{
    [SerializeField] GameObject light;
    [SerializeField] Animator animator;

    [SerializeField] bool saved;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        light = transform.GetChild(1).gameObject;

        saved = false;
    }

    private void Update()
    {
        if (saved)
        {
            animator.Play("CampfireAnimation");

            light.SetActive(true);
        }
        else if (!saved)
        {
            animator.Play("Camp-no-fire");

            light.SetActive(false);
        }
    }

    public void SetSaved(bool enable)
    {
        saved = enable;
    }
}
