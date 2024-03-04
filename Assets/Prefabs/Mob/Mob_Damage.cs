using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Damage : MonoBehaviour
{

    public float currentHealth = 100;
    public float MaxHealth = 100;

    public void MobTakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MobTakeDamage(10);
            print(currentHealth);
        }
    }
    }
