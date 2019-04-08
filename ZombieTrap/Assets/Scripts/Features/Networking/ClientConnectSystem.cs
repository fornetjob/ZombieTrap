using Game.Core.Networking;
using Game.Core.Networking.Udp;

using Entitas;
using System;

using Assets.Scripts.Features.Networking;

public class ClientConnectSystem : IExecuteSystem, IContextInitialize, ITearDownSystem
{
    #region Services

    private GameTimeService _gameTimeService = null;
    private SerializerService _serializerService = null;

    #endregion

    #region Factories

    private ConnectionStateFactory _connectionStateFactory = null;
    private MessageFactory _messageFactory = null;

    #endregion

    #region Fields

    private MessageContract
        _connectContract;

    private GameTimeEvent
        _tryTimeEvent;

    private ISender
        _sender;

    private GameEntity
        _stateEntity;

    #endregion

    #region IContextInitialize

    void IContextInitialize.Initialize(Contexts context)
    {
        _stateEntity = _connectionStateFactory.Create();

        _connectContract = _messageFactory.CreateConnectMessage(Guid.NewGuid());

        _tryTimeEvent = _gameTimeService.CreateTimeEvent(0.6f);

        _sender = new UdpSender(new SendConfiguration
        {
            RemoteHost = "localhost",
            RemotePort = 32100
        });

        _sender.Open();
    }

    #endregion

    public void Execute()
    {
        switch (_stateEntity.connectionState.value)
        {
            case ConnectionState.Connecting:
            case ConnectionState.Lost:
                if (_tryTimeEvent.Check())
                {
                    _sender.Send(_connectContract);

                    _stateEntity.ReplaceConnectionState(_stateEntity.connectionState.value, _stateEntity.connectionState.tryCount + 1);
                }
                break;
        }
    }

    #region ITearDownSystem

    void ITearDownSystem.TearDown()
    {
        _sender.Close();
    }

    #endregion
}