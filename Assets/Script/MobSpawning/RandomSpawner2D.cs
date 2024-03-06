using System.Collections;
using UnityEngine;

public class RandomSpawner2D : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnIntervalMin = 4f;
    public float spawnIntervalMax = 10f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
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

            int spawnCount = Random.Range(1, 3); // Générer 1 ou 2 objets
            for (int i = 0; i < spawnCount; i++)
            {
                Vector2 randomPosition = GenerateSpawnPosition(collider);
                if (randomPosition != Vector2.zero) // Vérifie si une position valide a été trouvée
                {
                    Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
                }
            }
        }
    }

    private Vector2 GenerateSpawnPosition(Collider2D collider)
    {
        // Initialiser randomPosition à Vector2.zero avant la boucle
        Vector2 randomPosition = Vector2.zero;
        bool positionFound = false;

        for (int tries = 0; tries < 100; tries++)
        {
            Vector2 potentialPosition = new Vector2(
                Random.Range(collider.bounds.min.x, collider.bounds.max.x),
                Random.Range(collider.bounds.min.y, collider.bounds.max.y)
            );

            // Vérifie s'il y a des colliders à cette position (comme des murs ou des obstacles)
            if (!Physics2D.OverlapCircle(potentialPosition, 0.5f)) // Ajustez le rayon selon la taille de vos mobs
            {
                randomPosition = potentialPosition; // Assigner la position valide trouvée à randomPosition
                positionFound = true;
                break; // Sortir de la boucle si une position valide est trouvée
            }
        }

        if (!positionFound)
        {
            Debug.LogWarning("Impossible de trouver une position valide pour le spawn après 100 essais");
            // Vous pouvez choisir de gérer différemment l'échec de trouver une position valide
        }

        return randomPosition; // Retourne la position trouvée ou Vector2.zero si aucune position valide n'est trouvée
    }

}
