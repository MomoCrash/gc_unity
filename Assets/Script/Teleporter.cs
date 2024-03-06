using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{

    public Transform Player;
    public Transform Camera;

    public int LevelID;
    public Vector2 nextSceneSpawnPoint;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("x"))
        {
            var position = new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"));
            Player.position = position;
            Camera.position = position;

            PlayerPrefs.DeleteKey("x");
            PlayerPrefs.DeleteKey("y");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("x", nextSceneSpawnPoint.x);
            PlayerPrefs.SetFloat("y", nextSceneSpawnPoint.y);
            SceneManager.LoadScene(LevelID);
        }
    }
}
