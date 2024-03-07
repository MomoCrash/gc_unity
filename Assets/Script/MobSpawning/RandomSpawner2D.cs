
using System.Collections;
using UnityEngine;

public class RandomSpawner2D : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnIntervalMin = 4f;
    public float spawnIntervalMax = 10f;
    private int currentMobCount = 0;
    public int maxMobCount = 5;

    public GameObject groundCheck;

    private void Start()
    {
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SpawnRoutine());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopCoroutine(SpawnRoutine());
        }
    }
    private IEnumerator SpawnRoutine()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.LogError("RandomSpawner2D nécessite un Collider2D pour fonctionner.");
            yield break;
        }

        while (true)
        {
            float waitTime = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(waitTime);

            if (currentMobCount < maxMobCount)
            {
                int spawnCount = Random.Range(1, 3);
                for (int i = 0; i < spawnCount && currentMobCount < maxMobCount; i++)
                {
                    Vector2 randomPosition = new Vector2(
                        Random.Range(collider.bounds.min.x, collider.bounds.max.x),
                        Random.Range(collider.bounds.min.y, collider.bounds.max.y)
                    );

                    //Deplace un objet invisible qui nous sert de repère de point de spawn
                    groundCheck.transform.position = randomPosition;
                    //OverlapCircle = crée un raycast en rond et renvoi une list de tous les objets touché
                    Collider2D[] touchedCollider = Physics2D.OverlapCircleAll(groundCheck.transform.position, 1f);

                    bool canSpawn = true;

                    //Si a touché des colliders
                    if (touchedCollider.Length > 0)
                    {
                        foreach (Collider2D touched in touchedCollider)
                        {
                            //Regarde leur tags
                            if (touched.gameObject.tag == "Ground" || touched.gameObject.tag == "wall" || touched.gameObject.tag == "UnderGround")
                            {

                                //Si est en contact avec un mur, sol ou sous sol, ne fais pas spawn l'ennemi
                                print("essaie de spawn dans un mur");
                                canSpawn = false;
                                break;
                            }
                        }
                    }

                    if (canSpawn)
                    {
                        GameObject spawnedMob = Instantiate(objectToSpawn, randomPosition, Quaternion.identity) as GameObject;
                        spawnedMob.transform.parent = transform;
                        currentMobCount++;
                    }
                }
            }
        }
    }


    public void DecrementMobCount()
    {
        if (currentMobCount > 0)
        {
            currentMobCount--;
        }
    }
}