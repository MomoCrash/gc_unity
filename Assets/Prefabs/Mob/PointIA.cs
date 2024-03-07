using UnityEngine;

public class PointIA : MobIA
{
    public GameObject PointA;
    public GameObject PointB;
    private Transform currentPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentPoint = PointB.transform;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, step);

        // Flip basé sur la direction du mouvement
        if (currentPoint == PointB.transform && transform.localScale.x < 0 ||
            currentPoint == PointA.transform && transform.localScale.x > 0)
        {
            Flip();
        }

        // Vérifie la distance avec le point courant pour changer de cible
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            currentPoint = currentPoint == PointA.transform ? PointB.transform : PointA.transform;
        }
    }

    void Flip()
    {
        // Multiplie la valeur x de localScale par -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);
        Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
    }
}
