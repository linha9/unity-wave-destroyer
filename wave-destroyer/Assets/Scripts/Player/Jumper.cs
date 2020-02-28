using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Jumper
{
    private float jumpForce;
    private int maxSequentJumps;
    private GroundChecker groundChecker;

    private bool isJumping;
    private int remainingJumps;

    private UnityEvent onGround;
    private UnityEvent onJumpPerformed;

    public Jumper(float jumpForce, int maxSequentJumps, GroundChecker groundChecker, UnityAction onJumpPerformed, UnityAction onGround)
    {
        this.jumpForce = jumpForce;
        this.maxSequentJumps = maxSequentJumps;
        this.groundChecker = groundChecker;

        this.onJumpPerformed = new UnityEvent();
        this.onJumpPerformed.AddListener(onJumpPerformed);
        this.onGround = new UnityEvent();
        this.onGround.AddListener(onGround);

        isJumping = false;
        remainingJumps = 0;
    }

    public void CaptureJump()
    {
        if (groundChecker.IsGrounded)
        {
            if (!Input.GetButton("Jump"))
            {
                remainingJumps = maxSequentJumps;
            }
            onGround.Invoke();
        }


        if (Input.GetButtonDown("Jump") && remainingJumps > 0) {
            remainingJumps -= 1;
            isJumping = true;
            onJumpPerformed.Invoke();
        }
    }

    public void Jump(Rigidbody2D rb2d)
    {
        if (isJumping)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce * Time.deltaTime);
            isJumping = false;
        }
    }
}