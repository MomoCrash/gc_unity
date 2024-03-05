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

    public float Resistance;
    public float FireResistance;
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
    bool Damage(float amount)
    {
        Health -= amount;
        healthBar.UpdateHealthBar(Health, MaxHealth);
        if (Health < 0) return true;
        return false;
    }

    void UpdateEquipment()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damage(20);
    }

}
