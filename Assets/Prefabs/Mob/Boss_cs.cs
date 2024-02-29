using Unity.VisualScripting;
using UnityEngine;

public class Boss_cs : MonoBehaviour
{
    public GameObject Player;
    public float speed;
    public float distanceBetween;
    public float currentHealth;
    public float attack;
    public float maxHealth;
    public float jumpForce = 1f; 
    public LayerMask obstacleLayer; 
    public LayerMask GroundMask;
    public float rayLength = 1f; 

    private Rigidbody2D rb;
    private float distance;

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
            
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, -speed * Time.deltaTime);
        }
        else if (distance > distanceBetween)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        }

        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
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
        
    }
}
