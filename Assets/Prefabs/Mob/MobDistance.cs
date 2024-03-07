using Unity.VisualScripting;
using UnityEngine;

public class MobDistance : MobIA
{

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
        Vector2 direction = (Player.transform.position - transform.position).normalized;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(direction.x, direction.y, 0) * rayLength);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Target was Hit!");
        }
        
    }
}
