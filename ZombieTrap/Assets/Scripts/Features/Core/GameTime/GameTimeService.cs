using UnityEngine;

public class GameTimeService : IService, IContextInitialize
{
    #region Fields

    private GameEntity
        _gameTimeEntity;

    #endregion

    void IContextInitialize.Initialize(Contexts context)
    {
        _gameTimeEntity = context.game.SetGameTime(0);
    }

    public float GetDeltaTime()
    {
        return Time.deltaTime;
    }

    public GameTimeEvent CreateTimeEvent(float timeInterval)
    {
        return new GameTimeEvent(_gameTimeEntity, timeInterval);
    }
}