using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeParalax : MonoBehaviour
{

    private static float Cooldown = .5f;

    float nextChange = 0;

    public GameObject Player;
    public GameObject Paralax;
    public Transform Camera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time > nextChange)
        {
            Destroy(Camera.GetChild(0).gameObject);

            var ParalaxObject = GameObject.Instantiate(Paralax, Camera);
            foreach (var child in ParalaxObject.GetComponentsInChildren<Paralax>())
            {
                child.cam = Camera.gameObject;
                child.player = Player;
            }
            nextChange = Time.time + Cooldown;
        }
    }
}
