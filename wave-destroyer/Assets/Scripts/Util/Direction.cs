using UnityEngine;

public static class Direction
{
    public static Dir floatToDir(float f)
    {
        if (f >= 0f)
        {
            return Dir.right;
        }
        else if (f <= 0f)
        {
            return Dir.left;
        }
        else
        {
            return Dir.none;
        }
    }

    public static float dirToFloat(Dir d)
    {
        if (d == Dir.left)
        {
            return -1f;
        } else if (d == Dir.right)
        {
            return 1f;
        } else if (d == Dir.any)
        {
            float[] possibilities = { 1f, -1f };
            return possibilities[Random.Range(0, possibilities.Length)];
        } else
        {
            return 0f;
        }
    }

    public static Dir switchDirection(Dir direction)
    {
        if (direction == Dir.left)
        {
            return Dir.right;
        }
        else if (direction == Dir.right)
        {
            return Dir.left;
        }
        else
        {
            return Dir.none;
        }
    }
}

public enum Dir
{
    left, right, none, any
}
