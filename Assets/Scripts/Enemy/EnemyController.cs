using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private EnemyHandle enemyHandle;

    private RangeAttack rangeAttack;

    private RangeHurt rangeHurt;

    private NavMeshAgent agent;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private float oldX;

    private Vector3 oldPosition;

    [SerializeField] private float kilometerToPlayer = 0f;

    [SerializeField] private float kilometerToOldPosition = 0f;

    [SerializeField] private GameObject player;

    [SerializeField] private RangeDetect rangeDetect;

    [SerializeField] private GameObject rangeHurtObject;

    [SerializeField] private ItemDrop itemDrop;

    // IF CAN REBORN, LET'S TICK IT
    public bool canReborn = false;
    private bool selectCanReborn = false;

    // =============== STATE VARIABLES ========================
    public enum State
    {
        Non,
        Idle,
        Patrol,
        Follow,
        Back,
        Attack,
        Hurt,
        Die,
        Reborn,
        Again
    }
    private State currentState = State.Non;
    public State changeState;
    // ======================================================

    // =============== STATE MACHINE ========================
    public void SwitchState(State newState)
    {
        if (newState != currentState)
        {
            switch (currentState)
            {
                case State.Idle:

                    break;
                case State.Patrol:

                    break;
                case State.Follow:

                    break;
                case State.Back:

                    break;
                case State.Attack:

                    break;
                case State.Hurt:
                    enemyHandle.TakeDamge(player.GetComponent<PlayerController>().GetStrength());
                    break;
                case State.Die:
                    // SET UP CAC THONG SO NHU BAN DAU
                    isBeforeReborn = false;
                    isDisappear = false;
                    isWaitToComeBack = false;
                    rangeHurtObject.SetActive(true);
                    rangeHurt.SetIsHurt(false);
                    canReborn = selectCanReborn;
                    break;
                case State.Reborn:

                    break;
                case State.Again:

                    break;
            }

            currentState = newState;
        }

        switch (newState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Patrol:

                break;
            case State.Follow:
                Follow();
                break;
            case State.Back:
                Back();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Hurt:
                Hurt();
                break;
            case State.Die:
                Die();
                break;
            case State.Reborn:
                Reborn();
                break;
            case State.Again:

                break;
        }
    }
    // ======================================================

    private void Awake()
    {
        enemyHandle = GetComponent<EnemyHandle>();

        rangeAttack = GetComponentInChildren<RangeAttack>();

        rangeHurt = GetComponentInChildren<RangeHurt>();

        rangeHurtObject = transform.GetChild(2).gameObject;

        rangeDetect = transform.parent.gameObject.GetComponentInChildren<RangeDetect>();

        animator = GetComponentInChildren<Animator>();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        itemDrop = GetComponent<ItemDrop>();

    }

    private void Start()
    {
        changeState = State.Idle;


        // NavMesh
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        //

        // Get Old Position;
        oldPosition = transform.position;

        // Get First position.x;
        oldX = transform.position.x;

        // SAVE OPTION canReborn
        selectCanReborn = canReborn;
    }

    private void Update()
    {
        // Convert Speed 
        agent.speed = enemyHandle.GetSpeed();
        //

        // Get Distance (Kilometer)
        KilometerToOldPosition();
        //

        FlipX();

        // HANDLE FROM PLAYER
        if (player != null)
        {
            SetOrderLayer();
            KilometerToPlayer();
        }

        SwitchState(changeState);

        if (rangeDetect.GetIsDetect())
        {
            // GET PLAYER FROM RANGE DETECT
            player = rangeDetect.GetComponent<RangeDetect>().player;

            if (enemyHandle.GetCurrentHealth() <= 0f)
            {
                if (canReborn)
                {
                    changeState = State.Reborn;
                }
                else
                {
                    changeState = State.Die;
                }
            }
            else if (rangeHurt.GetIsHurt())
            {
                changeState = State.Hurt;
            }
            else if (rangeAttack.GetIsAttack())
            {
                changeState = State.Attack;
            }
            else if (kilometerToPlayer <= 8f)
            {
                changeState = State.Follow;
            }
            else if (kilometerToOldPosition >= 1f)
            {
                changeState = State.Back;
            }
            else
            {
                changeState = State.Idle;
            }
        }
        else
        {
            changeState = State.Idle;
        }
    }

    private void SetOrderLayer()
    {
        Vector2 dir = player.transform.position - transform.position;
        if (dir.y > 0)
        {
            spriteRenderer.sortingOrder = 6;
        }
        else if (dir.y < 0)
        {
            spriteRenderer.sortingOrder = 4;
        }
    }

    private void FlipX()
    {
        if (oldX < transform.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (oldX > transform.position.x)
        {
            transform.localScale = new Vector2(1, 1);
        }

        oldX = transform.position.x;
    }

    private void Idle()
    {
        animator.Play("Idle");
    }

    private void Follow()
    {
        agent.SetDestination(player.transform.position);

        animator.Play("Run");
    }

    private void KilometerToPlayer()
    {
        kilometerToPlayer = Vector3.Distance(transform.position, player.transform.position);
    }

    private void Back()
    {
        agent.SetDestination(oldPosition);

        animator.Play("Run");
    }

    private void KilometerToOldPosition()
    {
        kilometerToOldPosition = Vector3.Distance(oldPosition, transform.position);
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);

        animator.Play("Attack");
    }

    private void Hurt()
    {
        agent.SetDestination(transform.position);

        animator.Play("Hurt");
    }

    private void Reborn()
    {
        agent.SetDestination(transform.position);

        if (!isBeforeReborn)
        {
            animator.Play("Die");
            rangeHurtObject.SetActive(false);


            isBeforeReborn = true;
            StartCoroutine(BeforeReborn());
        }
    }

    private bool isBeforeReborn = false;
    IEnumerator BeforeReborn()
    {
        yield return new WaitForSeconds(2.5f);
        animator.Play("Reborn");
        StartCoroutine(AfterReborn());
    }

    IEnumerator AfterReborn()
    {
        yield return new WaitForSeconds(1f);
        enemyHandle.SetCurrentHealth(enemyHandle.GetMaxHealth());
        canReborn = false;
        rangeHurtObject.SetActive(true);
    }

    private void Die()
    {
        agent.SetDestination(transform.position);


        if (!isDisappear)
        {
            animator.Play("Die");
            rangeHurtObject.SetActive(false);

            isDisappear = true;
            StartCoroutine(Disappear());
        }
    }

    private bool isDisappear = false;
    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(1f);

        // Roi vat pham 
        itemDrop.DropItem();
        //

        //Destroy(gameObject);

        if (!isWaitToComeBack)
        {
            isWaitToComeBack = true;

            //gameObject.SetActive(false);
            spriteRenderer.enabled = false;

            StartCoroutine(WaitToComeBack());
        }
    }

    private bool isWaitToComeBack = false;
    IEnumerator WaitToComeBack()
    {
        // THOI GIAN QUAI VAT TU RESET
        yield return new WaitForSeconds(30f);
        //

        // SET UP VE VI TRI CU~
        transform.position = oldPosition;

        // SET UP MAU VA ACTIVE
        enemyHandle.SetCurrentHealth(enemyHandle.GetMaxHealth());
        //gameObject.SetActive(true);
        spriteRenderer.enabled = true;
    }
}
