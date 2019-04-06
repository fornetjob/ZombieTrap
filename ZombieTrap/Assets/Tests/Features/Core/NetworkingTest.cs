using Assets.Scripts.Core.Networking;
using Assets.Scripts.Core.Networking.Udp;
using NUnit.Framework;
using System;

public class NetworkingTest
{
    [Test]
    public void Connect()
    {
        var messageFactory = new MessageFactory();

        using (var clientSender = new UdpSender(new SendConfiguration
        {
            RemoteHost = "localhost",
            RemotePort = 32100
        }))
        {
            using (var serverListener = new UdpListener(new ListenConfiguration
            {
                ListeningPort = 32100,
                ReceiveInterval = 10
            }))
            {
                bool isConnected = false;

                int timeOut = 100;

                serverListener.OnReceive += (endpoint, message) =>
                {
                    UnityEngine.Debug.Log(message.Type);

                    Assert.AreEqual(message.Type, MessageType.Connect);

                    isConnected = true;
                };

                serverListener.Open();
                clientSender.Open();

                var connectMsg = messageFactory.CreateConnectMessage(Guid.NewGuid());

                while (isConnected == false
                    && timeOut > 0)
                {
                    clientSender.Send(connectMsg);

                    timeOut--;

                    System.Threading.Thread.Sleep(10);
                }

                Assert.IsTrue(isConnected);
            }
        }
    }
}