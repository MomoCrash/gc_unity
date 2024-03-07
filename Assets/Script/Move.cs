using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Movement")]
    public float SprintSpeed = 1.3f;
    float MoveSpeed;

    [Header("Jump")]
    public float JumpForce = 1.0f;
    public float RestoreJumpDalay;
    public int currentJumpCount;
    int MaxJumpCount;

    [Header("Dash")]
    public int DashForce;
    public float RestoreDashDalay;
    public int currentDashCount;
    int MaxDashCount;

    Animator player_animator ;
    SpriteRenderer player_renderer ;

    [Header("Debug")]
    public Vector3 MoveVector = Vector3.zero;

    CameraFollow cameraFollow;

    public bool isJumping = false;

    public bool canUseNextJump = true;
    public bool canUseNextDash = true;

    public bool isOnGround = true;
    public bool isOnWall = false;

    Player player;

    float jumpUseWaitTime;
    float dashUseWaitTime;

    float dashRestoreTime;

    private void Start()
    {
        cameraFollow = FindFirstObjectByType<CameraFollow>();
        player_animator = gameObject.GetComponent<Animator>();
        player_renderer = gameObject.GetComponent<SpriteRenderer>();

        player = gameObject.GetComponent<Player>();

        currentJumpCount = player.MaxJumpCount;
        currentDashCount = player.MaxDashCount;

        UpdateStats();
    }

    void Update()
    {

        if (player.isDeath) return;

        var horz = Input.GetAxis("Horizontal");
        var jump = Input.GetAxis("Jump");
        var dash = Input.GetAxis("Dash");
        var sprint = Input.GetAxis("Sprint");

        bool IsMoving = -0.2f > horz || horz > 0.2f;
        bool IsSprinting = isOnGround && !isJumping && IsMoving && sprint != 0;
        bool CanDash =  dash != 0 && !IsSprinting && canUseNextDash && currentDashCount > 0;
        bool CanJump = (isOnGround || isJumping) && canUseNextJump && jump != 0 && currentJumpCount > 0;
        bool CanWallJump = !isJumping && canUseNextJump && jump != 0 && isOnWall;
        bool IsFalling = jump == 0 && !isOnGround && !isOnWall;

        MoveVector = Vector3.zero;

        if (horz > 0)
        {
            player_renderer.flipX = false ;
        } else if (horz < 0)
        {
            player_renderer.flipX = true ;
        }


        if (IsMoving)
        {
            MoveVector += new Vector3(horz * MoveSpeed, 0, 0);
            if (!IsSprinting)
            {
                player_animator.SetBool("IsRunning", false);
                player_animator.SetBool("IsWalking", true);
            }
        } else
        {
            player_animator.SetBool("IsWalking", false);
        }

        if (IsSprinting)
        {
            MoveVector *= SprintSpeed;
            player_animator.SetBool("IsRunning", true);
            player_animator.SetBool("IsWalking", false);
        } else
        {
            player_animator.SetBool("IsRunning", false);
        }

        if (CanDash)
        {
            if (isOnGround)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(horz * DashForce, 1, 0));
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(horz * DashForce / 2, -1, 0));
            }
            currentDashCount--;
            dashRestoreTime = Time.time + RestoreDashDalay;
            dashUseWaitTime = Time.time + .5f;
            canUseNextDash = false;
            player_animator.SetTrigger("Dash");
            return;
        }

        if (IsFalling)
        {
            player_animator.SetBool("IsFalling", true);
        } else
        {
            player_animator.SetBool("IsFalling", false);
        }

        if (CanJump)
        {
            Jump(new Vector3(0, jump * JumpForce * (1 + currentJumpCount / MaxJumpCount), 0), .3f);
            return;
        }

        if (CanWallJump)
        {
            Jump(new Vector3(horz * DashForce * 1, jump * JumpForce * 2, 0), .3f);
            return;
        }

    }

    private void Jump(Vector3 force, float waitToNextJump)
    {

        // Applied a force to the player
        gameObject.GetComponent<Rigidbody2D>().AddForce(force);

        // Update states of ground and wall detector to prevent collision bug
        isOnGround = false;
        isOnWall = false;
        isJumping = true;
        canUseNextJump = false;

        // Set the delay to use the next jump
        currentJumpCount--;
        jumpUseWaitTime = Time.time + waitToNextJump;
        player_animator.SetTrigger("Jump");

    }

    private void FixedUpdate()
    {
        transform.Translate(MoveVector * Time.deltaTime);

        RestoreUtilities();

        UpdateStats();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (isJumping)
            {
                cameraFollow.ShakeCamera(0, 1f, .25f);
                currentJumpCount = MaxJumpCount;
                isJumping = false;
            }
        } 
        if (collision.gameObject.CompareTag("Wall"))
        {
            isOnWall = true;
            isJumping = false;
            currentJumpCount = MaxJumpCount;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        } 
        if (collision.gameObject.CompareTag("Wall"))
        {
            isOnWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (isOnGround)
            {
                isOnGround = false;
            }
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            isOnWall = false;
        }
    }

    void UpdateStats()
    {
        MoveSpeed = player.Speed;

        MaxJumpCount = player.MaxJumpCount;
        MaxDashCount = player.MaxDashCount;
    }

    void RestoreUtilities()
    {

        if (Time.time >= jumpUseWaitTime)
        {
            canUseNextJump = true;
            jumpUseWaitTime = Time.time + jumpUseWaitTime;
        }

        if (Time.time >= dashUseWaitTime)
        {
            canUseNextDash = true;
            dashUseWaitTime = Time.time + dashUseWaitTime;
        }

        if (isOnGround)
        {

            if (dashRestoreTime != 0 && Time.time >= dashRestoreTime)
            {
                currentDashCount++;
                canUseNextDash = true;
                if (currentDashCount >= MaxDashCount)
                {
                    dashRestoreTime = 0;
                }
                else
                {
                    dashRestoreTime += Time.time + RestoreDashDalay;
                }
            }
        }
    }

}