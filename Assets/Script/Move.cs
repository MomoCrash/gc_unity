using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public float Force = 1.0f;
    public float JumpForce = 1.0f;

    bool isJumping = false;

    void Update()
    {

        var horz = Input.GetAxis("Horizontal");
        var jump = Input.GetAxis("Jump");

        print(isJumping);

        if (horz != 0)
        {
            gameObject.transform.Translate(new Vector3(horz * Force * Time.deltaTime, 0, 0));
        }
        if (jump != 0 && !isJumping)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, jump * JumpForce, 0));
            isJumping = true;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (isJumping)
            {
                isJumping = false;
            }
        }
    }

}
