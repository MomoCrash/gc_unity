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

    float jumpUseWaitTime;
    float dashUseWaitTime;

    float dashRestoreTime;

    private void Start()
    {
        cameraFollow = FindFirstObjectByType<CameraFollow>();
        player_animator = gameObject.GetComponent<Animator>();
        player_renderer = gameObject.GetComponent<SpriteRenderer>();

        var player = gameObject.GetComponent<Player>();

        MoveSpeed = player.Speed;

        MaxJumpCount = player.MaxJumpCount;
        MaxDashCount = player.MaxDashCount;

        currentJumpCount = player.MaxJumpCount;
        currentDashCount = player.MaxDashCount;
    }

    void Update()
    {

        var horz = Input.GetAxis("Horizontal");
        var jump = Input.GetAxis("Jump");
        var dash = Input.GetAxis("Dash");
        var sprint = Input.GetAxis("Sprint");

        bool IsMoving = horz != 0;
        bool IsSprinting = isOnGround && !isJumping && IsMoving && sprint != 0;
        bool CanDash = isOnGround && dash != 0 && !IsSprinting && canUseNextDash && currentDashCount > 0;
        bool CanJump = (isOnGround || isJumping) && canUseNextJump && jump != 0 && currentJumpCount > 0;
        bool CanWallJump = !isJumping && canUseNextJump && jump != 0 && isOnWall;

        print(IsMoving);
        print(CanWallJump);

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
                player_animator.SetTrigger("Walk");
            }
        }

        if (IsSprinting)
        {
            MoveVector *= SprintSpeed;
            player_animator.SetTrigger("Run");
        }

        if (CanDash)
        {
            print("Dash");
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(horz * DashForce, 0, 0));
            currentDashCount--;
            dashRestoreTime = Time.time + RestoreDashDalay;
            dashUseWaitTime = Time.time + .5f;
            canUseNextDash = false;
            return;
        }

        if (CanJump)
        {
            print("Jump");
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, jump * JumpForce * (1 + currentJumpCount/MaxJumpCount), 0));
            isJumping = true;
            currentJumpCount--;
            jumpUseWaitTime = Time.time + .3f;
            canUseNextJump = false;
            player_animator.SetTrigger("Jump");
            return;
        }

        if (CanWallJump)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(1*horz*DashForce, jump * JumpForce, 0));
            canUseNextJump = false;
            player_animator.SetTrigger("Jump");
            return;
        }

    }

    private void FixedUpdate()
    {
        transform.Translate(MoveVector * Time.deltaTime);

        RestoreUtilities();
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
            canUseNextJump =true;
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