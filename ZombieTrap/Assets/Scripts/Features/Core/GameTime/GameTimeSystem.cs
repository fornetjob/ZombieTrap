using Entitas;

public class GameTimeSystem : IExecuteSystem, IContextInitialize
{
    #region Services

    private GameTimeService
        _gameTimeService = null;

    #endregion

    #region Fields

    private GameEntity
        _timeEntity;

    #endregion

    void IContextInitialize.Initialize(Contexts context)
    {
        _timeEntity = context.game.gameTimeEntity;
    }

    public void Execute()
    {
        _timeEntity.ReplaceGameTime(_timeEntity.gameTime.value + _gameTimeService.GetDeltaTime());
    }
}