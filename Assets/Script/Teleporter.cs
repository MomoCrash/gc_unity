using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{

    public int LevelID;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        SceneManager.LoadScene(LevelID);

    }

}