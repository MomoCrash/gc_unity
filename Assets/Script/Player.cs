using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


public class Player : MonoBehaviour
{

    public float Heath;
    public float MaxHealth;

    public float Speed;
    public int MaxJumpCount;
    public int MaxDashCount;

    public float Resistance;
    public float FireResistance;
    public float EarthResistance;

    /// <summary>
    /// Damage the player and return if the player death
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    bool Damage(float amount)
    {
        Heath -= amount;
        if (Heath < 0) return true;
        return false;
    }

    void UpdateEquipment()
    {

    }

}
