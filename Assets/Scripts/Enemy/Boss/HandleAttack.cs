using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


namespace Enemy.Boss
{
    public class HandleAttack : MonoBehaviour
    {
        private float knockbackForce = 3.5f;
        public static int countHit = 0;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log(other.name);
                PerformKnockBack(other);
                countHit++;
            }
        }

        private void PerformKnockBack(Collider2D collision)
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            Vector3 force = direction * knockbackForce;
            Rigidbody2D player = collision.GetComponent<Rigidbody2D>();
            player.isKinematic = false;
            player.AddForce(force, ForceMode2D.Impulse);
            StartCoroutine(KnockBackReset(player));
        }

        private IEnumerator KnockBackReset(Rigidbody2D playerRb)
        {
            yield return new WaitForSeconds(1.5f);
            playerRb.velocity = Vector3.zero;
            playerRb.isKinematic = true;
        }
    }
}
