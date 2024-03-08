using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public GameObject Player;

    private Mob_Damage CurrentMob = null;

    public AudioSource AttackSound;

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && !Player.GetComponent<Player>().isActionInProgress)
        {
            AttackSound.PlayOneShot(AttackSound.clip, 0.2f);
            Player.GetComponent<Animator>().SetTrigger("Attack");
            if (Player.GetComponent<SpriteRenderer>().flipX)
            {
                Player.GetComponent<Rigidbody2D>().AddForce(new Vector3(-Player.GetComponent<Move>().DashForce/5, -1, 0));
            } else
            {
                Player.GetComponent<Rigidbody2D>().AddForce(new Vector3(Player.GetComponent<Move>().DashForce/5, -1, 0));
            }
            if (CurrentMob != null)
            {
                CurrentMob.MobTakeDamage(Player.GetComponent<Player>().BaseDamage * CurrentMob.Resistance);
            }
        }
    }

    private void FixedUpdate()
    {
        Player.GetComponent<SpriteRenderer>();
        if (!Player.GetComponent<SpriteRenderer>().flipX)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Mobs"))
        {
            CurrentMob = collision.gameObject.GetComponent<Mob_Damage>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Mobs"))
        {
            CurrentMob = null;
        }
    }
}

public enum AttackElement
{
    FIRE,
    BASIC,
    EARTH,
} 