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
    
    public void SyncTime(float serverTime)
    {
        _gameTimeEntity.ReplaceGameTime(serverTime);
    }

    public float GetGameTime()
    {
        return _gameTimeEntity.gameTime.value;
    }

    public float GetDeltaTime()
    {
        return Time.deltaTime;
    }

    public float GetFixedDeltaTime()
    {
        return Time.fixedDeltaTime;
    }

    public GameTimeEvent CreateTimeEvent(float timeInterval)
    {
        return new GameTimeEvent(_gameTimeEntity, timeInterval);
    }
}