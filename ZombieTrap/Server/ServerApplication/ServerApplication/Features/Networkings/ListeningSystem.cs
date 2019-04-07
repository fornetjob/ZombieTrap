using Assets.Scripts.Core.Networking;
using Assets.Scripts.Core.Networking.Udp;

using ServerApplication.Features.Networkings;

using System.Net;

public class ListeningSystem : IFixedExecuteSystem, IDependencyInitialize
{
    #region Services

    private SettingsService _settingsService = null;

    #endregion

    #region Processors

    private ServerMessageProcessor _serverMessageProcessor = null;

    #endregion

    #region Fields

    private IListener
        _listener;

    #endregion

    #region IDependencyInitialize

    void IDependencyInitialize.Initialize()
    {
        _listener = new UdpListener(new ListenConfiguration
        {
            ListeningPort = _settingsService.GetListeningPort(),
            ReceiveInterval = _settingsService.GetReceiveInterval()
        });
    }

    #endregion

    public void FixedExecute()
    {
    }

    private void OnMessageReceive(IPEndPoint ip, MessageContract msg)
    {
        _serverMessageProcessor.Process(ip, msg);
    }
}