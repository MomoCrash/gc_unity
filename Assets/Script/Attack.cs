using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public GameObject Player;

    private Mob_Damage CurrentMob = null;


    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Player.GetComponent<Animator>().SetTrigger("Attack");
            if (Player.GetComponent<SpriteRenderer>().flipX)
            {
                Player.GetComponent<Rigidbody2D>().AddForce(new Vector3(-Player.GetComponent<Move>().DashForce/3, -1, 0));
            } else
            {
                Player.GetComponent<Rigidbody2D>().AddForce(new Vector3(Player.GetComponent<Move>().DashForce/3, -1, 0));
            }
            if (CurrentMob != null)
            {
                CurrentMob.MobTakeDamage(Player.GetComponent<Player>().BaseDamage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Collide " + collision.tag);
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
