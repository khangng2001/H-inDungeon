using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject keng;

    public bool CLICK = false;

    private int staminaAttack1 = 2;
    private int staminaAttack2 = 4;

    private Animator ani;

    private int numClick = 0;
    [SerializeField] private AudioClip swordEffectSound;
    [SerializeField] private AudioClip swordStabEffectSound;
    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player.GetComponent<PlayerController>().GetStamina() >= staminaAttack1)
        {
            if (CLICK)
            {
                if (numClick == 0)
                {
                    Attack_1();
                    StartCoroutine(timeOfAttack_1());
                }
                numClick++;
                CLICK = false;
            }
        }
        else
        {
            numClick = 0;
        }
    }

    void Attack_1()
    {
        AudioManager.Instance.PlaySoundEffect(swordEffectSound);
        ani.Play("Attack_1");
        player.GetComponent<PlayerController>().DecreaseStamina(staminaAttack1);
    }

    void Attack_2()
    {
        ani.Play("Attack_2");
        AudioManager.Instance.PlaySoundEffect(swordStabEffectSound);
        player.GetComponent<PlayerController>().DecreaseStamina(staminaAttack2);
    }

    IEnumerator timeOfAttack_1()
    {
        yield return new WaitForSeconds(0.6f);

        if (numClick < 2)
        {
            numClick = 0;
        }
        else
        {
            if (player.GetComponent<PlayerController>().GetStamina() >= staminaAttack2)
            {
                Attack_2();
                StartCoroutine(timeOfAttack_2());
            }
            else
            {
                numClick = 0;
            }
        }
    }

    IEnumerator timeOfAttack_2()
    {
        yield return new WaitForSeconds(0.4f);

        numClick = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(keng, collision.ClosestPoint(transform.position), Quaternion.identity);
    }
}
