using UnityEngine;

public class Mob_Flying : MonoBehaviour
{
    public GameObject player;
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public float pathfindingUpdateInterval = 0.5f; // Temps en secondes entre les mises � jour de pathfinding
    public float distanceToStop = 1f; // Arr�te le mob � cette distance du joueur

    private float timeSinceLastPathfindingUpdate = 0f;
    private Vector3 targetPosition;

    void Start()
    {
        // Initialise le pathfinding imm�diatement
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
        // Arr�tez le mouvement si le mob est suffisamment proche du joueur
        if (Vector3.Distance(transform.position, targetPosition) < distanceToStop) return;

        // D�placez le mob vers le joueur
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Tournez le mob vers le joueur
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }
}
