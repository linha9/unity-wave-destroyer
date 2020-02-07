using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class XMovement
{
    public const float DIR_LEFT = -1f;
    public const float DIR_RIGHT = 1f;
    public const float DIR_NONE = 0f;

    public float Speed { get; private set; }

    public float XInput { get; private set; }
    public float Direction { get; private set; }
    public bool IsMoving { get; private set; }

    public XMovement(float speed)
    {
        Speed = speed;
    }

    public void CaptureXInput()
    {
        XInput = Input.GetAxis("Horizontal");
        IsMoving = (Mathf.Approximately(XInput, 0f)) ? (false) : (true);
        Direction = CalculateDirection();
    }

    public void Stop()
    {
        XInput = 0f;
        IsMoving = false;
        Direction = DIR_NONE;
    }

    public void Move(Rigidbody2D rb2d)
    {
        rb2d.velocity = new Vector2(XInput * Speed * Time.deltaTime, rb2d.velocity.y);
    }

    private int CalculateDirection()
    {
        if (XInput < 0f)
        {
            return -1;
        }
        else if (XInput > 0f)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
