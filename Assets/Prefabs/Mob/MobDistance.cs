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

    // Update is called once per frame
    void Update()
    {
       
        distance = Vector2.Distance(transform.position, Player.transform.position);


        Vector2 direction = (Player.transform.position - transform.position).normalized;
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
            animator.SetTrigger("Run");
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, -speed * Time.deltaTime);
        }
        else if (distance > distanceBetween)
        {
            animator.SetTrigger("Run");
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        }

        else
        {
            animator.SetTrigger("NotRunning");
        }

        
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
