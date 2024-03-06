using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Damage : MonoBehaviour
{

    public ItemStack[] items;

    public MobIA MobIA;

    [Range(0f, 1f)]
    public float Resistance;
   
    public float currentHealth = 100;
    public float MaxHealth = 100;

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
    public void MobTakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, MaxHealth);
        if (currentHealth <= 0)
        {
            MobIA.MobDeath();
            Invoke("DestroyMob", 0.65f);
        }
    }

    private void DestroyMob()
    {
        foreach (var item in items)
        {
            DropItem.DropItemInWorld(GameObject.Find("Items"), gameObject.transform.position, GameObject.Find("itemexemple"), item);
        }
        Destroy(gameObject);
    }
}
