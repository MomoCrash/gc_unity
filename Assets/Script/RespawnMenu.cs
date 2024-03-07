using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    public void Respawn()
    {
        SceneManager.LoadScene(1);
    }
}

