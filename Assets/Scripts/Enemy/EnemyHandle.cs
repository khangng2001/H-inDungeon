using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandle : MonoBehaviour
{
    public float strengthEnemy = 0f;
    public float speedEnemy = 0f;
    public float healththEnemy = 0f;

    private void Start()
    {
        SetStrength(strengthEnemy);
        SetSpeed(speedEnemy);
        SetMaxHealth(healththEnemy);
        SetCurrentHealth(healththEnemy);
    }

    // ==================== HANDLE ===================
    // ======================================================

    // ==================== HANDLE STRENGTH ===================
    private float strength = 0f;

    public void SetStrength(float newStrength)
    {
        strength = newStrength;
    }

    public float GetStrength()
    {
        return strength;
    }

    public void IncreaseStrength(float addStrength)
    {
        strength += addStrength;
    }

    public void DecreaseStrength(float lostStrength)
    {
        strength -= lostStrength;
    }
    // ======================================================

    // ==================== HANDLE SPEED ===================
    private float speed = 0f;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void IncreaseSpeed(float addSpeed)
    {
        speed += addSpeed;
    }

    public void DecreaseSpeed(float lostSpeed)
    {
        speed -= lostSpeed;
    }

    // ======================================================

    // ================== HANDLE HEALTH ======================
    private float maxHealth = 0;
    [SerializeField]private float currentHealth = 0;

    public void SetMaxHealth(float newHealth)
    {
        maxHealth = newHealth;
    }

    public void SetCurrentHealth(float newHealth)
    {
        currentHealth = maxHealth;
    }

    public void TakeDamge(float lostHealth)
    {
        currentHealth -= lostHealth;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    // ======================================================
}
