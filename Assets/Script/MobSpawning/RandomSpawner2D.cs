using System.Collections;
using UnityEngine;

public class RandomSpawner2D : MonoBehaviour
{
    public GameObject objectToSpawn; // L'objet à faire apparaître
    public float spawnIntervalMin = 4f; // Temps minimum entre les spawns
    public float spawnIntervalMax = 10f; // Temps maximum entre les spawns

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        // Assurez-vous que le composant est bien un Collider2D
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.LogError("RandomSpawner2D nécessite un Collider2D pour fonctionner.");
            yield break;
        }

        while (true) // Boucle infinie pour continuer à faire apparaître des objets
        {
            float waitTime = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(waitTime); // Attendre un temps aléatoire entre spawnIntervalMin et spawnIntervalMax

            int spawnCount = Random.Range(1, 3); // Générer soit 1 soit 2 objets
            for (int i = 0; i < spawnCount; i++)
            {
                // Générer une position aléatoire à l'intérieur du collider du trigger
                Vector2 randomPosition = new Vector2(
                    Random.Range(collider.bounds.min.x, collider.bounds.max.x),
                    Random.Range(collider.bounds.min.y, collider.bounds.max.y)
                );

                // Faire apparaître l'objet à la position aléatoire
                Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
            }
        }
    }
}
