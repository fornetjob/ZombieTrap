using Game.Core.Networking.Udp;
using UnityEngine;

public class NetworkSettingsService : IDependency
{
    private const string ListenConfigurationKey = "ListenConfiguration";
    private const string SenderConfigurationKey = "SenderConfiguration";

    public ListenConfiguration GetListenConfiguration()
    {
        if (PlayerPrefs.HasKey(ListenConfigurationKey))
        {
            return (ListenConfiguration)JsonUtility.FromJson(
                PlayerPrefs.GetString(ListenConfigurationKey), typeof(ListenConfiguration));
        }
        else
        {
            return new ListenConfiguration
            {
                ListeningPort = 32000,
                ReceiveInterval = 10
            };
        }
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