using System;
using System.Collections;
using UnityEngine;

namespace Enemy.Boss
{
    public class DragonStates : MonoBehaviour
    {
        private Animator animator;
        
        [Header("Handle States")]
        [SerializeField] private float moveSpeed = 2f;
        private  Vector3 moveDir = Vector3.zero;
        private readonly Vector2 boxSize = new Vector2(25f, 15f);
        [SerializeField] private Transform detector;
        [SerializeField] private LayerMask playerMask;
        [SerializeField] private Transform target;
        
        [Header("Checking condition")]
        [SerializeField] private bool canDetect;
        [SerializeField] private bool canHover;
        private Collider2D hit; 
        private void Awake()
        {
            animator = GetComponent<Animator>();
            if(target == null) return;
        }
        
        private enum States
        {
            Idle,
            Walk,
            Melee,
            Hover,
            Launch,
            Fly,
            Landing,
            Firebreath
        }

        [SerializeField] private States currentState;

        private void UpdateState(States newState)
        {
            switch (newState)
            {
                case States.Idle:
                    animator.Play("Idle");
                    break;
                case States.Walk:
                    animator.Play("Moving");
                    break;
                case States.Melee:
                    animator.Play("Melee");
                    break;
                case States.Hover:
                    animator.Play("Hover");
                    break;
                case States.Launch:
                    animator.Play("Launch");
                    break;
                case States.Fly:
                    animator.Play("Fly");
                    break;
                case States.Landing:
                    animator.Play("Landing");
                    break;
                case States.Firebreath:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
                break;
            }
            currentState = newState;
        }

        private void Start()
        {
            UpdateState(States.Hover);
            StartCoroutine(Landing());
        }

        private void Update()
        {
            PerformDetection();
            FlipX();
            PerformMeteor();
        }

        private IEnumerator Landing()
        {
            yield return new WaitForSeconds(3f);
            UpdateState(States.Landing);
            yield return new WaitForSeconds(1f);
            UpdateState(States.Idle);
            canDetect = true;
        }

        private void PerformDetection()
        {
            if (canDetect)
            {
                hit = Physics2D.OverlapBox(detector.transform.position, boxSize, 0f, playerMask);
                if (hit != null)
                {
                    target = hit.transform;
                    SetOrderLayer();
                    HandleDetection();
                }
            }
        }

        private void HandleDetection()
        {
            if(target == null) return;
            if (CalDistance() > 2f && !canHover)
            {
                UpdateState(States.Walk);
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            }
            else if (CalDistance() <= 2f && !canHover)
            {
                UpdateState(States.Melee);
            }
        }
        
        private float CalDistance()
        {
            return Vector2.Distance(detector.position, target.position);
        }

        private void FlipX()
        {
           
            if (target != null)
            {
                moveDir = target.position - transform.position;
                if (moveDir.x < 1f)
                    transform.localScale = new Vector3(1f, 1f, 1f);
                else if (moveDir.x > 1f)
                    transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }

        private void SetOrderLayer()
        {
            Vector2 dir = target.position - transform.position;
            if (dir.y > 0)
            {
                GetComponent<SpriteRenderer>().sortingOrder = 6;
            }
            else if (dir.y < 0)
            {
                GetComponent<SpriteRenderer>().sortingOrder = 4;
            }
        }

        private void PerformMeteor()
        {
            if (HandleAttack.countHit == 5)
            {
                canHover = true;
                UpdateState(States.Hover);
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(detector.position,boxSize);
            //Gizmos.DrawWireSphere(attacker.position,0.5f);
        }

        
    }
}
