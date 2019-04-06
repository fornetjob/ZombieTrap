using UnityEngine;

public class ResourceService : IService
{
    private WeakDictionary<string, Sprite>
        _spriteDict = new WeakDictionary<string, Sprite>((path) => Resources.Load<Sprite>(path));

    public Sprite GetSprite(string path)
    {
        return _spriteDict[path];
    }
}