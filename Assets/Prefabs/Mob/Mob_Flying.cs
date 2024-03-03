using UnityEngine;

public class Mob_Flying : MonoBehaviour
{
    public GameObject player;
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public float pathfindingUpdateInterval = 0.5f; // Temps en secondes entre les mises à jour de pathfinding
    public float distanceToStop = 1f; // Arrête le mob à cette distance du joueur

    private float timeSinceLastPathfindingUpdate = 0f;
    private Vector3 targetPosition;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        UpdatePathfinding();
    }

    void Update()
    {
        timeSinceLastPathfindingUpdate += Time.deltaTime;

        if (timeSinceLastPathfindingUpdate >= pathfindingUpdateInterval)
        {
            UpdatePathfinding();
            timeSinceLastPathfindingUpdate = 0f;
        }

        MoveTowardsTarget();
    }

    void UpdatePathfinding()
    {
        targetPosition = player.transform.position;
    }

    void MoveTowardsTarget()
    {
        if (Vector3.Distance(transform.position, targetPosition) < distanceToStop) return;

        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

}
