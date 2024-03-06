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
        print(Health);
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

    public void SavePlayer ()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer ()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        Health = data.Health;
        Vector3 position;

        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
}
