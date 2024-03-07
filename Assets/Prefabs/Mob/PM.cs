using UnityEngine;

public class PM : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float shootingDistance = 5f; // La distance de tir
    private Transform player; // R�f�rence au joueur
    private float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Trouver le joueur
    }

    void Update()
    {
        // Calcule la distance entre le joueur et le monstre
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        timer += Time.deltaTime;

        // Si le joueur est � port�e et que 2 secondes se sont �coul�es, tire
        if (distanceToPlayer <= shootingDistance && timer > 2)
        {
            timer = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
    }
}
