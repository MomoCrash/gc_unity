using UnityEngine;
using System.Collections;

public class PM : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;


    private float timer;
    // Unity Message References
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            timer = 0;
            shoot();
        }
    }

    void shoot()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
    }
}

