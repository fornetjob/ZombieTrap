using UnityEngine;

public class RandomService : IService
{
    public int Range(int min, int max)
    {
        return Random.Range(min, max);
    }

    public float Range(float min, float max)
    {
        return Random.Range(min, max);
    }

    public Vector2 RandomDir()
    {
        return new Vector2(Range(-1f, +1f), Range(-1f, +1f));
    }

    public Vector2 RandomPos(Bounds bound)
    {
        return new Vector2(Range(bound.min.x, bound.max.x), Range(bound.min.y, bound.max.y));
    }
}