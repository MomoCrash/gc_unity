using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class NPC : MonoBehaviour
{
    public float MaxMoveAround;
    public float DelayMove;

    float currentMovedDistance;
    float startPositionX;

    bool canInteract = false;

    Vector2 nextMove = Vector2.zero;

    private void Start()
    {
        startPositionX = transform.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canInteract = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canInteract = false;
    }

    IEnumerator MoveDelayer()
    {

        while (true)
        {

            float moveTime = 0;
            if (nextMove == Vector2.zero || moveTime >= DelayMove)
            {

                float maxNextMove = (startPositionX+MaxMoveAround) - currentMovedDistance;
                nextMove = new Vector2(Random.Range(-maxNextMove, maxNextMove), 0);

            }
            else
            {

                moveTime += DelayMove / 60;
                transform.position = Vector2.Lerp(nextMove, transform.position, .95f);
                yield return new WaitForSeconds(DelayMove/60);

            }

        }

    }

}
