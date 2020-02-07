using UnityEngine;

[System.Serializable]
public class DashMovement
{
    private enum DashStates
    {
        ALLOWED, BLOCKED, WAITING_INPUT, DASHING
    }

    public float DashForce { get; private set; }
    private DashStates dashState;
    private CountdownTimer blockTimer;
    private CountdownTimer waitInputTimer;

    private KeyCode prevPress;
    private float direction;

    private KeyCode rightKey = KeyCode.D;
    private KeyCode leftKey = KeyCode.A;

    public DashMovement(float dashForce, float blockTime, float waitInputTime)
    {
        DashForce = dashForce;
        blockTimer = new CountdownTimer(blockTime);
        waitInputTimer = new CountdownTimer(waitInputTime);
    }

    public void CaptureDash()
    {
        if (dashState == DashStates.ALLOWED)
        {
            if (Input.GetKeyDown(rightKey))
            {
                prevPress = rightKey;
                dashState = DashStates.WAITING_INPUT;
            }
            else if (Input.GetKeyDown(leftKey))
            {
                prevPress = leftKey;
                dashState = DashStates.WAITING_INPUT;
            }
        }
        else if (dashState == DashStates.WAITING_INPUT)
        {
            if (waitInputTimer.Count())
            {
                dashState = DashStates.ALLOWED;
            }
            else if (Input.GetKeyDown(rightKey) && prevPress == rightKey)
            {
                dashState = DashStates.DASHING;
                direction = 1f;
            }
            else if (Input.GetKeyDown(leftKey) && prevPress == leftKey)
            {
                dashState = DashStates.DASHING;
                direction = -1f;
            }
            else if (Input.GetKeyDown(rightKey) && prevPress == leftKey)
            {
                dashState = DashStates.ALLOWED;
            }
            else if (Input.GetKeyDown(leftKey) && prevPress == rightKey)
            {
                dashState = DashStates.ALLOWED;
            }
        }
        else if (dashState == DashStates.BLOCKED)
        {
            if (blockTimer.Count())
            {
                dashState = DashStates.ALLOWED;
            }
        }
    }

    public void Dash(Rigidbody2D rb2d)
    {
        if (dashState == DashStates.DASHING)
        {
            rb2d.AddForce(new Vector2(direction * DashForce, 0f));
            dashState = DashStates.BLOCKED;
        }

    }
}
