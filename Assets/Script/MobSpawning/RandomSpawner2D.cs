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
            Debug.LogError("RandomSpawner2D n�cessite un Collider2D pour fonctionner.");
            yield break;
        }

        while (true)
        {
            float waitTime = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(waitTime);

            int spawnCount = Random.Range(1, 3); // G�n�rer 1 ou 2 objets
            for (int i = 0; i < spawnCount; i++)
            {
                Vector2 randomPosition = GenerateSpawnPosition(collider);
                if (randomPosition != Vector2.zero) // V�rifie si une position valide a �t� trouv�e
                {
                    Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
                }
            }
        }
    }

    private Vector2 GenerateSpawnPosition(Collider2D collider)
    {
        // Initialiser randomPosition � Vector2.zero avant la boucle
        Vector2 randomPosition = Vector2.zero;
        bool positionFound = false;

        for (int tries = 0; tries < 100; tries++)
        {
            Vector2 potentialPosition = new Vector2(
                Random.Range(collider.bounds.min.x, collider.bounds.max.x),
                Random.Range(collider.bounds.min.y, collider.bounds.max.y)
            );

            // V�rifie s'il y a des colliders � cette position (comme des murs ou des obstacles)
            if (!Physics2D.OverlapCircle(potentialPosition, 0.5f)) // Ajustez le rayon selon la taille de vos mobs
            {
                randomPosition = potentialPosition; // Assigner la position valide trouv�e � randomPosition
                positionFound = true;
                break; // Sortir de la boucle si une position valide est trouv�e
            }
        }

        if (!positionFound)
        {
            Debug.LogWarning("Impossible de trouver une position valide pour le spawn apr�s 100 essais");
            // Vous pouvez choisir de g�rer diff�remment l'�chec de trouver une position valide
        }

        return randomPosition; // Retourne la position trouv�e ou Vector2.zero si aucune position valide n'est trouv�e
    }

}
