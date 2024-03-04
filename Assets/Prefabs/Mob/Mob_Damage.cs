using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Damage : MonoBehaviour
{

    public MobIA MobIA;
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
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, MaxHealth);
        print(currentHealth);
        if (currentHealth <= 0)
        {
            MobIA.MobDeath();
            Invoke("DestroyMob", 0.65f);
        }
    }

    private void DestroyMob()
    {
        Destroy(gameObject);
        print("detruit");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MobTakeDamage(Choosedamage);
        }
    }
}
