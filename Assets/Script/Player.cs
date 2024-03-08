using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public AudioSource DeathSound;

    public float HealTime;
    public float AddWaitTime;

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

    public bool isActionInProgress;

    public bool isDeath;

    private void Start()
    {
        Health = MaxHealth;
        healthBar.UpdateHealthBar(Health, MaxHealth);
        StartCoroutine(HealhPlayer());
    }

    public void Damage(float amount, AttackElement element)
    {
        if (isDeath) return;
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
        AddWaitTime = 4f;
        Health -= amount;
        healthBar.UpdateHealthBar(Health, MaxHealth);
        if (Health <= 0) Death();
    }

    public void Death()
    {
        isDeath = true;
        gameObject.GetComponent<Animator>().SetTrigger("Death");
        DeathSound.Play();
        StartCoroutine(DelayDeath());
    }

    IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(3);
    }
    
    IEnumerator HealhPlayer()
    {
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        yield return new WaitForSeconds(HealTime);
        if (Health+MaxHealth/1000 <= MaxHealth)
        {
            Health += MaxHealth / 1000;
            healthBar.UpdateHealthBar(Health, MaxHealth);
            yield return new WaitForSeconds(AddWaitTime);
            AddWaitTime = 0;
        }
        StartCoroutine(HealhPlayer());
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
