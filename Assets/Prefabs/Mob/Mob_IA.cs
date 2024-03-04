using Unity.VisualScripting;
using UnityEngine;

public class Mob_IA : MonoBehaviour
{
    public Player Player;
    public float speed;
    public float distanceBetween;
    public float attack;
    public float jumpForce = 1f;
    public LayerMask obstacleLayer; 
    public LayerMask GroundMask;
    public float rayLength = 1f;

    private Rigidbody2D rb;
    private float distance;
    private bool isJumping = false;
    private Animator animator;

    public bool grounded;

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
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(5, 5, 5); // Orientation vers la droite
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-5, 5, 5); // Orientation vers la gauche
        }

        if (distance < distanceBetween)
        {
            // Déplacement vers le joueur
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

    // Dessiner le raycast dans l'éditeur pour le débogage
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        // Assurez-vous que la direction du Gizmo correspond à la direction vers le joueur.
        Vector2 direction = (Player.transform.position - transform.position).normalized;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(direction.x, direction.y, 0) * rayLength);
    }

    public void mobDeath()
    {
        animator.SetTrigger("Death");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            grounded = true;
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            grounded = false;
            isJumping = true;
        }
    }
}
