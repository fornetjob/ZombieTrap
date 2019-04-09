using Game.Core.Networking.Udp;
using System.Threading;
using UnityEngine;

public class NetworkSettingsService : IDependency
{
    private const string ListenConfigurationKey = "ListenConfiguration";
    private const string SenderConfigurationKey = "SenderConfiguration";

    private Mutex
        _portMutex;

    private ListenConfiguration
        _listenConfiguration;

    public ListenConfiguration GetListenConfiguration()
    {
        if (_listenConfiguration == null)
        {
            if (PlayerPrefs.HasKey(ListenConfigurationKey))
            {
                _listenConfiguration = (ListenConfiguration)JsonUtility.FromJson(
                    PlayerPrefs.GetString(ListenConfigurationKey), typeof(ListenConfiguration));
            }
            else
            {
                _listenConfiguration = new ListenConfiguration
                {
                    ListeningPort = 32000,
                    ReceiveInterval = 10
                };
            }

            for (int i = 0; i < 1000; i++)
            {
                string mutexName = string.Format("ZombieTrapPort_{0}", _listenConfiguration.ListeningPort + i);

                bool isNew;

                _portMutex = new Mutex(true, mutexName, out isNew);

                if (isNew)
                {
                    _listenConfiguration.ListeningPort += i;
                    break;
                }
            }
        }

        return _listenConfiguration;
    }

    public SendConfiguration GetSenderConfiguration()
    {
        if (PlayerPrefs.HasKey(SenderConfigurationKey))
        {
            return (SendConfiguration)JsonUtility.FromJson(
                PlayerPrefs.GetString(SenderConfigurationKey), typeof(SendConfiguration));
        }
        else
        {
            return new SendConfiguration
            {
                RemoteHost = "localhost",
                RemotePort = 32100
            };
        }
    }
}