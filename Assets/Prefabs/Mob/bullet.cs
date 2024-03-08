using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float Damage;

    public float life = 3;
    public float force;

    private GameObject Player;
    private Rigidbody2D rb;
    public float rotationSpeed = 1.0f;


    void Start()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = Player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

    }
    void FixedUpdate()
    {
        transform.Rotate(0, 0 , rotationSpeed);
    }

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Damage(Damage, AttackElement.EARTH);
        }
    }

}
