using Game.Core;
using System;

public class RandomService:IService
{
    private Random _rnd = new Random();

    public int Range(int min, int max)
    {
        return _rnd.Next(min, max);
    }

    public float Range(float min, float max)
    {
        var next = _rnd.NextDouble();

        return (float)(min + (next * (max - min)));
    }

    public Vector2Float RandomDir()
    {
        return new Vector2Float(Range(-1f, +1f), Range(-1f, +1f));
    }

    public Vector2Float RandomPos(RectangleFloat bound)
    {
        return new Vector2Float(Range(bound.min.x, bound.max.x), Range(bound.min.y, bound.max.y));
    }

    public ItemType GetRandomZombie()
    {
        ItemType item;

        switch (Range(0, 3))
        {
            case 0:
                item = ItemType.SmallZombie;
                break;
            case 1:
                item = ItemType.MediumZombie;
                break;
            default:
                item = ItemType.BigZombie;
                break;
        }

        return item;
    }
}