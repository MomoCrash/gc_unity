﻿using Unity.VisualScripting;
using UnityEngine;

public class FollowIA : MobIA
{

    public float AttackSpeed = 0.2f;

    float nextAttack;
    bool canAttack;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, Player.transform.position);
        Vector2 direction = (Player.transform.position - transform.position).normalized;

        // Tourner le monstre vers le joueur
        Flip(direction.x);

        if (distance < distanceBetween)
        {
            // Deplacement vers le joueur
            animator.SetTrigger("Run");
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, obstacleLayer);
            if (hit.collider != null && !isJumping)
            {

                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
            }
        }
        else
        {
            animator.SetTrigger("NotRunning");
        }
    }

    // Dessiner le raycast dans l'�diteur pour le d�bogage
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        // Assurez-vous que la direction du Gizmo correspond � la direction vers le joueur.
        Vector2 direction = (Player.transform.position - transform.position).normalized;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(direction.x, direction.y, 0) * rayLength);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.Damage(attack, element);
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            grounded = true;
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            canAttack = false;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            grounded = false;
            isJumping = true;
        }
    }
}
