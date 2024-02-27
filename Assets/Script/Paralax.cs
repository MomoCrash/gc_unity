using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{

    private float Lenght, StartPositionX, StartPositionY;
    public GameObject cam;

    [SerializeField] float parallaxEffect;

    private void Start()
    {
        StartPositionX = transform.position.x;
        StartPositionY = transform.position.y;
        Lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        var tempX = cam.transform.position.x * (1 - parallaxEffect);
        var distanceX = cam.transform.position.x * parallaxEffect;
        var distanceY = cam.transform.position.y * parallaxEffect;

        transform.position = new Vector3(StartPositionX + distanceX, StartPositionY + distanceY, transform.position.z);

        if (tempX > StartPositionX + Lenght)
        {
            StartPositionX += Lenght;
        } else if (tempX < StartPositionX - Lenght)
        {
            StartPositionX -= Lenght;
        }
    }


}
