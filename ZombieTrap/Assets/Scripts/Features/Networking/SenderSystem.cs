using Game.Core.Networking;
using Game.Core.Networking.Udp;

using Entitas;
using System;

using Assets.Scripts.Features.Networking;

public class ServerConnectionSystem : IExecuteSystem, IContextInitialize, ITearDownSystem
{
    #region Services

    private GameTimeService _gameTimeService = null;
    private NetworkSettingsService _networkSettingsService = null;

    #endregion

    #region Poolings

    private SenderMessagesPooling _senderMessagesPooling = null;

    #endregion

    #region Factories

    private ConnectionStateFactory _connectionStateFactory = null;
    private MessageFactory _messageFactory = null;

    #endregion

    #region Fields

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

        _tryTimeEvent = _gameTimeService.CreateTimeEvent(0.6f);

        _sender = new UdpSender(_networkSettingsService.GetSenderConfiguration());

        _sender.Open();

        _messageFactory.CreateConnectMessage();
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
                    _messageFactory.CreateConnectMessage();

                    _stateEntity.ReplaceConnectionState(_stateEntity.connectionState.value, _stateEntity.connectionState.tryCount + 1);
                }
                break;
        }

        if (_senderMessagesPooling.IsHasMessages())
        {
            _sender.Send(_senderMessagesPooling.Dequeue());
        }
    }

    #region ITearDownSystem

    void ITearDownSystem.TearDown()
    {
        _sender.Close();
    }

    #endregion
}