using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    [Range(0f, 1f)]
    public float Smooth;

    void FixedUpdate()
    {
        var moveVector = Vector3.Lerp(player.position, gameObject.transform.position, Smooth);
        moveVector.z = -10;
        gameObject.transform.position = moveVector;
    }
}
