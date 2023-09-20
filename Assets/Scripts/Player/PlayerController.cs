using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;
using Realms.Sync.Exceptions;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    // FADE BLACK
    [SerializeField] private GameObject fadeDie;
    // HEALTH PLAYER
    [SerializeField] private float maxHealth = 0f;
    private float currentHealth = 0f;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private TextMeshProUGUI textHealthBar;
    [SerializeField] private GameObject arrow;

    // STAMINA PLAYER
    [SerializeField] private float maxStamina = 0f;
    private float currentStamina = 0f;
    [SerializeField] private GameObject staminaBar;
    [SerializeField] private TextMeshProUGUI textStaminaBar;
    
    // STRENGTH PLAYER
    [SerializeField] private float strength = 0f;

    // BLOOD PARTICLE
    [SerializeField] private GameObject bloodObject;


    private float countTimeIncreaseStamina = 0f;

    [SerializeField] private Animator animator;

    // PLAYER'S COMPONENT
    private PlayerInput playerInput;
    private PlayerController playerController;

    [SerializeField]private float moveSpeed = 5f;
    private Vector3 moveDir = Vector3.zero;
    public static PlayerController instance;
    private bool canMove = true;

    [SerializeField] private Vector3 checkPoint;
    [SerializeField] private bool hasTouchedCheckpoint;
    [SerializeField] private Vector3 currentCheckPoint;
    [SerializeField] private int oldCheckPointIndex;

    private Transform transformDragon;
    private bool isRepel = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        arrow.SetActive(false);
        animator = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerController = GetComponent<PlayerController>();
        SetMaxHealth(maxHealth);
        SetStamina(maxStamina);
        SetStrength(strength);

        currentCheckPoint = new Vector3(0f, 0f, 0f);
        checkPoint = new Vector3(-10.75f, 3.84f, 0f);
        oldCheckPointIndex = 3;
    }

    //private void OnEnable()
    //{

    //    SceneManager.sceneLoaded += OnSceneLoaded;

    //}

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}

    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    this.gameObject.SetActive(true);
    //}

    void Update()
    {
        if (DialogueManager.instance.isDialoguePlaying)
        {
            return;
        }

        LifeController();

        if (canMove)
        {
            Moving();

            FlipXByMouse();

            FlipYByMouse();
        }

        IncreaseStaminaByTime();

        if (isRepel)
        {
            Repel();
        }
    }

    void LifeController()
    {
        if (GetCurrentHealth() <= 0)
        {
            //CLEAR ALL ITEM
            InventoryManager.instance.ClearAllItem();

            animator.Play("Die");
            playerInput.enabled = false;
            //playerController.enabled = false;
            transform.GetChild(1).gameObject.SetActive(false); // LIGHT 2D (Tat den cua Ngo Tat To)

            if (!isFadeOut)
            {
                isFadeOut = true;

                // KHONG THE DI CHUYEN NUA
                canMove = false;

                fadeDie.GetComponent<Animator>().Play("FadeOut");

                fadeDie.transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = 200;

                StartCoroutine(AfterFadeOut());
            }
        }
    }

    private bool isFadeOut = false;
    IEnumerator AfterFadeOut()
    {
        yield return new WaitForSeconds(2f);

        ResetPlayer();
        fadeDie.GetComponent<Animator>().Play("FadeIn");

        fadeDie.transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = 0;

    }

    public void ResetPlayer()
    {
        

        // SET UP LAI VI TRI SAU KHI PLAY AGAIN

        //transform.position = currentCheckPoint;
        AudioManager.Instance.StopFightMusic();
        SceneManager.LoadScene(oldCheckPointIndex);
        transform.position = checkPoint;

        //if (hasTouchedCheckpoint)
        //{
        //    transform.position = checkPoint.transform.position;
        //}
        //else
        //{
        //    transform.position = GameManager.instance.entrances[GameManager.instance.CurrentIndexEntrance];
        //}
        //DataPersistence.instance.LoadGame();
        //SceneManager.LoadScene(2);


        // SET UP LAI THONG SO
        SetMaxHealth(maxHealth);
        SetStamina(maxStamina);
        SetStrength(strength);

        // INPUT TRO LAI
        playerInput.enabled = true;

        // SETE UP LAI FADE DE CO THE SU DUNG LAI
        isFadeOut = false;

        // MO DEN
        transform.GetChild(1).gameObject.SetActive(true);

        // DI CHUYEN LAI BINH THUONG
        canMove = true;
    }

    public void SetCurrentCheckPoint (Vector3 newCheckPoint)
    {
        currentCheckPoint = newCheckPoint;
    }

    public Vector3 GetCurrentCheckPoint()
    {
        return currentCheckPoint;
    }

    void FlipXByMouse()
    {
        Vector2 direction = playerInput.inputMosue - new Vector2(transform.position.x, transform.position.y);

        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void FlipYByMouse()
    {
        Vector2 direction = playerInput.inputMosue - new Vector2(transform.position.x, transform.position.y);

        if (direction.y < 0)
        {
            animator.Play("DownRight");
        }
        else if (direction.y > 0)
        {
            animator.Play("UpRight");
        }
    }

    private void Moving()
    {
        float horizontal = playerInput.horizontal;
        float vertical = playerInput.vertical;

        moveDir.Set(horizontal, vertical, 0f);
        moveDir.Normalize();

        transform.Translate( moveDir * (moveSpeed * Time.deltaTime));
    }

    public void BloodOut()
    {
        Instantiate(bloodObject, transform.position, Quaternion.identity);
    }

    private void Repel() // BI DAY LUI 
    {
        transform.position = Vector2.MoveTowards(transform.position, transformDragon.position, -5f * Time.deltaTime);
    }

    IEnumerator TimeRepel()
    {
        yield return new WaitForSeconds(1f);

        isRepel = false;

        playerInput.enabled = true;
    }

    // KHI PLAYER BI DAY LUI VA CO VA CHAM THI KHONG DAY LUI NUA
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isRepel = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAttackRange"))
        {
            DecreaseHealth(collision.gameObject.GetComponentInParent<EnemyHandle>().GetStrength());
            BloodOut();

            if (collision.transform.parent.gameObject.name.Equals("Dragon"))
            {
                transformDragon = collision.transform.parent.gameObject.transform;
                isRepel = true;
                playerInput.enabled = false;
                StartCoroutine(TimeRepel());
            }
        }
        else if(collision.CompareTag("Checkpoint"))
        {
            if (collision.gameObject.name.Equals("Campfire"))
            {
                collision.gameObject.GetComponent<DetectAndSave>().SetSaved(true);
            }

            hasTouchedCheckpoint = true;
            oldCheckPointIndex = GameManager.instance.GetSceneIndex();
            checkPoint = collision.gameObject.GetComponent<Transform>().transform.position;
            DataPersistence.instance.SaveGame();
        }
        else if (collision.CompareTag("PortalLevel1"))
        {
            StartCoroutine(GameManager.instance.WaitOnLoad());
        }
        else if (collision.CompareTag("DisplayArrow"))
        {
            arrow.SetActive(true);
            Invoke("DisplayArrow", 10);
        }
    }

    private void DisplayArrow()
    {
        arrow.SetActive(false);
    }

    // ================= HANDLE HEALTH ======================
    public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
        healthBar.GetComponent<Slider>().maxValue = maxHealth;
        LoadHealth();
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void LoadHealth()
    {
        textHealthBar.text = currentHealth + "/" + maxHealth;
        healthBar.GetComponent<Slider>().value = currentHealth;
    }

    public void IncreaseHealth(float newHealth)
    {
        if (currentHealth <= maxHealth)
        {
            currentHealth += newHealth;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }

        LoadHealth();
    }

    public void DecreaseHealth(float lostHealth)
    {
        if (currentHealth > 0)
        {
            currentHealth -= lostHealth;

            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
        }

        LoadHealth();
    }

    public void SetCurrentHealth(float newCurrentHealth)
    {
        currentHealth = newCurrentHealth;
        LoadHealth();
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    // ==========================================

    // ======= HANDLE STAMINA ==============
    public void SetStamina(float newStamina)
    {
        maxStamina = newStamina;
        currentStamina = maxStamina;
        staminaBar.GetComponent<Slider>().maxValue = maxStamina;
        LoadStamina();
    }

    public void LoadStamina()
    {
        textStaminaBar.text = currentStamina + "/" + maxStamina;
        staminaBar.GetComponent<Slider>().value = currentStamina;
    }

    public void IncreaseStamina(float newStamina)
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += newStamina;

            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
        }

        LoadStamina();
    }

    public void DecreaseStamina(float lostStamina)
    {
        if (currentStamina >= 0)
        {
            currentStamina -= lostStamina;

            if (currentStamina < 0)
            {
                currentStamina = 0;
            }
        }

        LoadStamina();
    }

    public float GetStamina()
    {
        return currentStamina;
    }

    private void IncreaseStaminaByTime()
    {
        if (currentStamina < maxStamina)
        {
            countTimeIncreaseStamina += Time.deltaTime;

            if (countTimeIncreaseStamina > 0.5f)
            {
                IncreaseStamina(1);

                countTimeIncreaseStamina = 0;
            }
        }
    }
    // ===========================================

    // ======= HANDLE STRENGTH ==============
    public void SetStrength(float newStrength)
    {
        strength = newStrength;
    }

    public void IncreaseStength(float newStrength)
    {
        strength += newStrength;
    }

    public void DecreaseStength(float lostStrength)
    {
        if (strength >= 0)
        {
            strength -= lostStrength;

            if (strength < 0)
            {
                strength = 0;
            }
        }
    }

    public float GetStrength()
    {
        return strength;
    }
    // ===========================================

    // ======= HANDLE SPEED ==============
    public float GetSpeed()
    {
        return moveSpeed;
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public void SetCanMove(bool enable)
    {
        canMove = enable;
    }

    // ===========================================

    // ======================= SAVE AND LOAD DATA =========================
    public void LoadData(GameData data)
    {
        try
        {
            //player's position
            transform.position = new Vector3(data.Position.X, data.Position.Y, data.Position.Z);

            //Health
            SetCurrentHealth(data.Health);

            //Damage
            SetStrength(data.Damage);

            //Checkpoint
            checkPoint = new Vector3(data.Position.X, data.Position.Y, data.Position.Z);
        }
        catch (AppException ex)
        {
            Debug.LogException(ex);
        }
    }

    public void SaveData(ref GameData data)
    {
        try
        {
            //player's position
            data.Position = new Position
            {
                X = transform.position.x,
                Y = transform.position.y,
                Z = transform.position.z
            };

            //Health
            data.Health = GetCurrentHealth();

            //Damage
            data.Damage = GetStrength();
        }
        catch (AppException ex)
        {
            Debug.LogException(ex);
        }


    }
    
    
}
