using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


public class Player : MonoBehaviour
{

    public float Health;
    public float MaxHealth;

    public float Speed;
    public int MaxJumpCount;
    public int MaxDashCount;

    public float BaseDamage;

    [Range(0f, 1f)]
    public float Resistance;
    [Range(0f, 1f)]
    public float FireResistance;
    [Range(0f, 1f)]
    public float EarthResistance;
    [SerializeField] FloatingHealthBar healthBar;

    private void Start()
    {
        Health = MaxHealth;
        healthBar.UpdateHealthBar(Health, MaxHealth);
    }

    /// <summary>
    /// Damage the player and return if the player death
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    bool Damage(float amount, AttackElement element)
    {
        switch (element)
        {
            case AttackElement.FIRE:
                Health -= amount * FireResistance;
                break;
            case AttackElement.EARTH:
                Health -= amount * EarthResistance;
                break;
            case AttackElement.BASIC:
                Health -= amount * Resistance;
                break;
            default: break;
        }
        Health -= amount;
        healthBar.UpdateHealthBar(Health, MaxHealth);
        if (Health < 0) return true;
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mobs"))
        {
            var mobIA = collision.gameObject.GetComponent<MobIA>();
            if (Damage(mobIA.attack, mobIA.element))
            {
                gameObject.GetComponent<Animator>().SetTrigger("Death");
            }
        }
    }

}
