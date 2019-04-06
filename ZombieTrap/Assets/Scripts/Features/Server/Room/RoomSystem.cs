using Entitas;
using UnityEngine;

namespace Assets.Scripts.Features.Server.Room
{
    public class RoomSystem : IExecuteSystem, IInitializeSystem
    {
        #region Factories

        private ServerBoardFactory _boardFactory = null;
        private GameTimeService _gameTimeService = null;
        private ZombieService _zombieService = null;

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
                && _zombieService.IsWantSpawn(_roomEntity))
            {
                _zombieService.SpawnZombie(_roomEntity);
            }
        }
    }
}
