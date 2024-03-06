using UnityEngine;

[System.Serializable]
public class MobIA : MonoBehaviour
{

    public Player Player;
    public float speed;
    public float distanceBetween;
    public float attack;
    public AttackElement element;
    public float jumpForce;
    public LayerMask obstacleLayer;
    public LayerMask GroundMask;
    public float rayLength;

    public Rigidbody2D rb;
    public float distance;
    public bool isJumping;
    public Animator animator;

    public bool grounded;

    public void MobDeath()
    {
        animator.SetTrigger("Death");
    }

    public void Flip(float orientation)
    {
        if (orientation < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        } else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

}