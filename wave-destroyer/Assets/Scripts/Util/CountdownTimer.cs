using UnityEngine;

[System.Serializable]
public class CountdownTimer
{
    private float totalTime;
    public float Countdown { get; private set; }

    public CountdownTimer(float totalTime)
    {
        this.totalTime = totalTime;
        Countdown = totalTime;
    }

    public void Reset()
    {
        Countdown = totalTime;
    }

    public bool Count()
    {
        if (Countdown <= 0f)
        {
            Countdown = totalTime;
            return true;
        }
        else
        {
            Countdown -= Time.deltaTime;
            return false;
        }
    }

    public bool Count(float dTime)
    {
        if (Countdown <= 0f)
        {
            Countdown = totalTime;
            return true;
        }
        else
        {
            Countdown -= dTime;
            return false;
        }
    }
}
