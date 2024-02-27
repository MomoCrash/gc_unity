using Unity.VisualScripting;
using UnityEngine;

public class Mob_IA : MonoBehaviour
{
    public GameObject Player;
    public float speed;
    public float distanceBetween;
    public float currentHealth;
    public float attack;
    public float maxHealth;
    public float jumpForce = 1f; // Force de saut à régler
    public LayerMask obstacleLayer; // Layer pour identifier les obstacles
    public LayerMask GroundMask;
    public float rayLength = 1f; // Longueur du rayon pour détecter les obstacles

    private Rigidbody2D rb;
    private float distance;
    private bool isJumping = false;

    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, Player.transform.position);
        Vector2 direction = (Player.transform.position - transform.position).normalized;

        if (distance < distanceBetween)
        {
            // Déplacement vers le joueur
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);


            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, obstacleLayer);
            if (hit.collider != null && !isJumping)
            {
                // Appliquer la force de saut une seule fois
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true; // Le monstre est maintenant en train de sauter
                print(isJumping);
            }
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

    void MobDeath()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Target was Hit!");
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            isJumping = true;
        }
    }
}
