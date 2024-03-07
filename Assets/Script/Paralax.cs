using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{

    private float Lenght, StartPositionX, StartPositionY;
    public GameObject cam;
    public GameObject player;

    [SerializeField] float parallaxEffect;

    float SpriteSizeY;

    private void Start()
    {
        StartPositionX = transform.position.x;
        StartPositionY = transform.position.y;
        Lenght = GetComponent<SpriteRenderer>().bounds.size.x;
        SpriteSizeY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void FixedUpdate()
    {
        var tempX = cam.transform.position.x * (1 - parallaxEffect);
        var distanceX = cam.transform.position.x * parallaxEffect;
        var y = cam.transform.position.y;
        var distanceY = -(Mathf.Exp(y) / (y + Mathf.Exp(Mathf.Pow(y, 2))) );

        transform.position = new Vector3(StartPositionX + distanceX, y, transform.position.z);

        if (tempX > StartPositionX + Lenght)
        {
            StartPositionX += Lenght;
        } else if (tempX < StartPositionX - Lenght)
        {
            StartPositionX -= Lenght;
        }
    }


}
