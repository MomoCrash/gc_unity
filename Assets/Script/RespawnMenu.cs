using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    // Assuming you have a spawn point set up in your scene
    public Transform spawnPoint;

    // The method to call when your respawn button is pressed
    public void Respawn()
    {
        // Reset the player's position to the spawn point
        gameObject.transform.position = spawnPoint.position;
        gameObject.transform.rotation = spawnPoint.rotation;

        // You can also reset health or other stats if needed
        // For example: playerHealth.ResetToFull();

        // If you want to reset the entire scene instead:
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Optionally, you could call this in response to a player's death
    // public void OnPlayerDeath()
    // {
    //     // Disable player controls, show respawn UI, etc.
    // }
}

