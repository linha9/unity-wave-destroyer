using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float xSpeed = 100f;
    [SerializeField]
    private float dashForce = 3000f;
    [SerializeField]
    private float dashBlockTime = 1f;
    [SerializeField]
    private float dashWaitInputTime = 0.4f;
    [SerializeField]
    private float jumpForce = 20f;
    [SerializeField]
    private int maxSequentJumps = 2;

    private GroundChecker groundChecker;

    private Rigidbody2D rb2d;
    private Animator anim;

    private XMovement xMover;
    private Jumper jumper;
    private DashMovement dashMover;

    private bool isFacingRight;
    private bool isSit;
   
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundChecker = GetComponentInChildren<GroundChecker>();
    }

    private void Start()
    {
        xMover = new XMovement(xSpeed);
        dashMover = new DashMovement(dashForce, dashBlockTime, dashWaitInputTime);
        jumper = new Jumper(jumpForce, maxSequentJumps, groundChecker, OnJumpPerformed, OnGround);
    }

    private void Update()
    {
        HandleSit();
        if (!isSit)
        {
            xMover.CaptureXInput();
            dashMover.CaptureDash();

            HandleFlip();
            HandleAttack();

            if (xMover.IsMoving)
            {
                anim.SetBool("Run", true);
            }
            else
            {
                anim.SetBool("Run", false);
            }
        }
    }

    private void LateUpdate()
    {
        jumper.CaptureJump();
    }

    private void OnJumpPerformed()
    {
        anim.SetBool("Jump", true);
    }

    private void OnGround()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            anim.SetBool("Jump", false);
        }
    }

    private void HandleSit()
    {
        if (Input.GetButtonDown("Sit"))
        {
            if (isSit)
            {
                isSit = false;
                anim.Play("Idle");
            }
            else if (groundChecker.IsGrounded && !isSit)
            {
                isSit = true;
                xMover.Stop();
                anim.Play("Sit");
            }
        }
    }

    private void HandleAttack() {
        if (Input.GetKey(KeyCode.Mouse0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.SetTrigger("Attack");
        }
        else
        {
            anim.ResetTrigger("Attack");
        }
    }

    private void HandleFlip()
    {
        if (xMover.Direction == XMovement.DIR_LEFT && isFacingRight)
        {
            Flip();
        }
        else if (xMover.Direction == XMovement.DIR_RIGHT && !isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale *= new Vector2(-1, 1);
    }

    private void FixedUpdate()
    {
        xMover.Move(rb2d);
        dashMover.Dash(rb2d);
        jumper.Jump(rb2d);

        ImproveFallsAndUps();
    }

    private void ImproveFallsAndUps()
    {
        if (!Mathf.Approximately(rb2d.velocity.y, 0f))
        {
            if (rb2d.velocity.y < 0f)
            {
                this.rb2d.velocity -= new Vector2(0f, 0.2f);
            }
            else if (rb2d.velocity.y > 0f)
            {
                if (!Input.GetButton("Jump"))
                {
                    this.rb2d.velocity -= new Vector2(0f, 0.2f);
                }
            }
        }
    }
}
