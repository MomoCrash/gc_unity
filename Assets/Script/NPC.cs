using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class NPC : MonoBehaviour
{
    public float MaxMoveAround;
    public float DelayMove;
    [Range(0f, 1f)]
    public float Speed;

    float currentMovedDistance;
    float startPositionX;

    bool canInteract = false;
    bool generateNewMovement;

    Vector2 nextMove = Vector2.zero;

    private void Start()
    {
        StartCoroutine(MoveDelayer());
    }

    private void FixedUpdate()
    {

        if (generateNewMovement)
        {

            startPositionX = transform.position.x;
            float maxNextMove = (startPositionX + MaxMoveAround) - currentMovedDistance;
            var nextMoveForce = Random.Range(-maxNextMove, maxNextMove);
            nextMove = new Vector2(nextMoveForce, 0);
            generateNewMovement = false;
            if (nextMoveForce < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            } else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

        }
        else
        {

            var currentPos = new Vector2(transform.position.x, 0);
            var distanceToNext = (nextMove - currentPos);

            if (distanceToNext.x < 1f)
            {
                GetComponent<Animator>().SetBool("Walk", false);
            } else
            {
                GetComponent<Animator>().SetBool("Walk", true);
                transform.Translate(distanceToNext * Time.deltaTime / DelayMove);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = false;
        }
    }

    IEnumerator MoveDelayer()
    {

        yield return new WaitForSeconds(DelayMove);
        generateNewMovement = true;
        StartCoroutine(MoveDelayer());

    }

}
