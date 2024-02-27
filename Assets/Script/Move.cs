using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [Header("Movement")]
    public float MoveSpeed = 3.0f;
    public float SprintSpeed = 1.3f;

    [Header("Jump")]
    public float JumpForce = 1.0f;
    public int MaxJumpCount;
    public float RestoreJumpDalay;
    public int currentJumpCount;

    [Header("Dash")]
    public int DashForce;
    public int MaxDashCount;
    public float RestoreDashDalay;
    int currentDashCount;

    [Header("Debug")]
    public Vector3 MoveVector = Vector3.zero;

    CameraFollow cameraFollow;

    bool isJumping = false;

    bool canUseNextJump = true;
    bool canUseNextDash = true;

    bool isOnGround = true;
    bool isOnWall = false;

    float jumpUseWaitTime;
    float dashUseWaitTime;

    float jumpRestoreTime;
    float dashRestoreTime;

    private void Start()
    {
        cameraFollow = FindFirstObjectByType<CameraFollow>();

        currentJumpCount = MaxJumpCount;
        currentDashCount = MaxDashCount;
    }

    void Update()
    {

        var horz = Input.GetAxis("Horizontal");
        var jump = Input.GetAxis("Jump");
        var dash = Input.GetAxis("Dash");
        var sprint = Input.GetAxis("Sprint");

        print(sprint);

        bool IsMoving = horz != 0;
        bool IsSprinting = isOnGround && !isJumping && IsMoving && sprint != 0;
        bool CanDash = dash != 0 && !IsSprinting && canUseNextDash && currentDashCount > 0;
        bool CanJump = isOnGround && jump != 0 && canUseNextJump && currentJumpCount > 0;

        MoveVector = Vector3.zero;


        if (IsMoving)
        {
            MoveVector += new Vector3(horz * MoveSpeed, 0, 0);
        }

        if (IsSprinting)
        {
            MoveVector *= (SprintSpeed * sprint);
        }

        if (CanDash)
        {
            MoveVector += new Vector3(dash * DashForce, 0, 0);
            currentDashCount--;
            dashRestoreTime = Time.time + RestoreDashDalay;
            dashUseWaitTime = Time.time + .3f;
            canUseNextDash = false;
        }

        if (CanJump)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, jump * JumpForce, 0));
            isJumping = true;
            currentJumpCount--;
            jumpRestoreTime = Time.time + RestoreJumpDalay;
            jumpUseWaitTime = Time.time + .3f;
            canUseNextJump = false;
        }

    }

    private void FixedUpdate()
    {
        transform.Translate(MoveVector * Time.deltaTime);

        RestoreUtilities();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnWall = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (isJumping)
            {
                cameraFollow.ShakeCamera(0, 1f, .25f);
                currentJumpCount = MaxJumpCount;
            }
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
            isOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (isOnGround)
            {
                isOnGround = true;
            }
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

        if (isOnGround && dashRestoreTime != 0 || jumpRestoreTime != 0)
        {
            if (Time.time >= jumpRestoreTime)
            {
                currentJumpCount++;
                canUseNextJump = true;
                if (currentJumpCount >= MaxJumpCount)
                {
                    jumpRestoreTime = 0;
                }
                else
                {
                    jumpRestoreTime += Time.time + RestoreDashDalay;
                }
            }

            if (Time.time >= dashRestoreTime)
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