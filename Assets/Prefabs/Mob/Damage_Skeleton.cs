using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Damage : MonoBehaviour
{
    public AI_Patrol MobIA;
    public float currentHealth = 100;
    public float MaxHealth = 100;
    public int Choosedamage;

    [SerializeField] FloatingHealthBar healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    private void Start()
    {
        currentHealth = MaxHealth;
        healthBar.UpdateHealthBar(currentHealth, MaxHealth);

    }
    public void MobTakeDamage(int damage)
    {
        print("takeDaamge");
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, MaxHealth);
        print(currentHealth);
        if (currentHealth <= 0)
        {
            MobIA.mobDeath();
            Invoke("destroyMob", 0.65f);
        }
    }

    private void destroyMob()
    {
        Destroy(gameObject);
        print("detruit");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("InCollider");
            MobTakeDamage(Choosedamage);
        }
    }
}
