using Game.Core.Networking;
using Game.Core.Networking.Udp;

using Entitas;

using System.Net;
using Assets.Scripts.Features.Networking;

public class ProcessMessagesSystem : IExecuteSystem, IContextInitialize, ITearDownSystem
{
    #region Services

    private GameTimeService _gameTimeService = null;
    private ClientMessageProcessor _messageProcessor = null;
    private NetworkSettingsService _networkSettingsService = null;

    #endregion

    #region Factories

    private MessageFactory _messageFactory = null;

    #endregion

    #region Poolings

    private SenderMessagesPooling _senderMessagesPooling = null;
    private ListenMessagesPooling _listenMessagePoolings = null;

    #endregion

    #region Fields

    private IListener
        _listener;

    private GameEntity
        _stateEntity;

    private GameTimeEvent
        _connectionTimeEvent;
    
    #endregion

    #region IContextInitialize

    void IContextInitialize.Initialize(Contexts context)
    {
        _stateEntity = context.game.connectionStateEntity;

        _connectionTimeEvent = _gameTimeService.CreateTimeEvent(1);

        _listener = new UdpListener(_networkSettingsService.GetListenConfiguration());

        _listener.OnReceive += OnMessageReceive;

        _listener.Open();
    }

    #endregion

    #region Event handlers

    private void OnMessageReceive(IPEndPoint ip, MessageContract msg)
    {
        if (_stateEntity.connectionState.value != ConnectionState.Active)
        {
            _stateEntity.ReplaceConnectionState(ConnectionState.Active, 0);
        }

        _connectionTimeEvent.Reset();

        _listenMessagePoolings.Enqueue(msg);

        if (msg.Type.IsStrongMessage())
        {
            _messageFactory.CreateReplyMessage(msg.Id);
        }
    }

    #endregion

    public void Execute()
    {
        if (_connectionTimeEvent.Check()
            && _stateEntity.connectionState.value == ConnectionState.Active)
        {
            _stateEntity.ReplaceConnectionState(ConnectionState.Lost, 0);

            // Мы потеряли соединение, очистим очередь
            _listenMessagePoolings.Clear();
            _senderMessagesPooling.Clear();
        }
        else
        {
            MessageContract msg;

            if (_listenMessagePoolings.TryDequeueMessage(out msg))
            {
                _messageProcessor.Process(msg);
            }
        }
    }

    #region ITearDownSystem

    void ITearDownSystem.TearDown()
    {
        _listener.Close();

        _stateEntity.ReplaceConnectionState(ConnectionState.Closed, 0);
    }

    #endregion
}