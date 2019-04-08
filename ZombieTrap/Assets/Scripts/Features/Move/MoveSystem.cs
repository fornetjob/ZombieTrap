using Entitas;
using UnityEngine;

public class MoveSystem : IExecuteSystem
{
    #region Services

    private GameTimeService _gameTimeService = null;

    #endregion

    [Group(GameComponentsLookup.Move)]
    private IGroup<GameEntity> _moves = null;

    public void Execute()
    {
        if (_moves.count > 0)
        {
            var moveEntities = _moves.GetEntities();

            var time = _gameTimeService.GetDeltaTime();

            for (int i = 0; i < moveEntities.Length; i++)
            {
                var moveEntity = moveEntities[i];

                var pos = moveEntity.position.value;

                var speed = moveEntity.move.speed;
                var posTo = moveEntity.move.posTo;

                pos = Vector3.MoveTowards(pos, posTo, time * speed);

                if (Vector3.Distance(pos, posTo) <= Mathf.Epsilon)
                {
                    pos = posTo;

                    moveEntity.RemoveMove();
                }

                moveEntity.ReplacePosition(pos);
            }
        }
    }
}