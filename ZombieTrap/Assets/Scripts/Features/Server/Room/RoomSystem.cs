using Assets.Scripts.Features.Core.Zombies;
using Entitas;
using UnityEngine;

namespace Assets.Scripts.Features.Server.Room
{
    public class RoomSystem : IExecuteSystem, IInitializeSystem
    {
        #region Services

        private RandomService _randomService = null;
        private GameTimeService _gameTimeService = null;
        private RoomBoundService _roomBoundService = null;

        #endregion

        #region Factories

        private ServerZombieFactory _serverZombieFactory = null;
        private ServerBoardFactory _boardFactory = null;

        #endregion

        #region Groups

        [Group(GameComponentsLookup.Zombie)]
        private IGroup<GameEntity> _zombies = null;

        #endregion

        #region Fields

        private ServerSideEntity
            _roomEntity;

        private GameTimeEvent
            _spawnTimeEvent;

        #endregion

        public RoomSystem(ServerSideEntity room)
        {
            _roomEntity = room;
        }

        void IInitializeSystem.Initialize()
        {
            _boardFactory.Create(_roomEntity.bound.value);

            _spawnTimeEvent = _gameTimeService.CreateTimeEvent(_roomEntity.room.SpawnTimeInterval);
        }

        public void Execute()
        {
            if (_spawnTimeEvent.Check()
                && IsWantSpawn())
            {
                SpawnZombie();
            }
        }

        #region Private methods

        private bool IsWantSpawn()
        {
            return _zombies.count < _roomEntity.room.MaxZombieCount;
        }

        private bool IsZombieIntersect(Vector3 pos, float radius)
        {
            if (_zombies.count > 0)
            {
                var zombies = _zombies.GetEntities();

                for (int i = 0; i < zombies.Length; i++)
                {
                    var zombie = zombies[i];

                    var otherPos = zombie.position.value;
                    var otherRadius = zombie.zombie.radius;

                    var distBetweenZombies = Vector2.Distance(pos, otherPos);

                    if (distBetweenZombies < radius + otherRadius)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void SpawnZombie()
        {
            float radius;
            ZombieType type;

            switch (_randomService.Range(0, 3))
            {
                case 0:
                    type = ZombieType.Small;
                    radius = 0.4f;
                    break;
                case 1:
                    type = ZombieType.Medium;
                    radius = 0.5f;
                    break;
                default:
                    type = ZombieType.Big;
                    radius = 0.6f;
                    break;
            }

            var spawnBound = _roomBoundService.GetRoomBound(radius);

            var pos = _randomService.RandomPos(spawnBound);

            if (IsZombieIntersect(pos, radius) == false)
            {
                _roomEntity.ReplaceIdentity(_roomEntity.identity.value + 1);

                _serverZombieFactory.Create(_roomEntity.identity.value, type, radius, pos);
            }
        }

        #endregion
    }
}
