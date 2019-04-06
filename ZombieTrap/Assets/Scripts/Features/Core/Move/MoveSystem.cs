using Entitas;
using UnityEngine;

namespace Assets.Scripts.Features.Core.Move
{
    public class MoveSystem : IExecuteSystem
    {
        #region Services

        private GameTimeService _gameTimeService = null;

        #endregion

        #region Group

        [Group(GameComponentsLookup.Move)]
        private IGroup<GameEntity> _moveGroup = null;

        #endregion

        public void Execute()
        {
            if (_moveGroup.count > 0)
            {
                var time = _gameTimeService.GetDeltaTime();

                var moveEntities = _moveGroup.GetEntities();

                for (int moveIndex = 0; moveIndex < moveEntities.Length; moveIndex++)
                {
                    var moveEntity = moveEntities[moveIndex];

                    var speed = moveEntity.move.speed;
                    var posTo = moveEntity.move.posTo;

                    var pos = moveEntity.position.value;

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
}