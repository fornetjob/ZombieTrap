using Assets.Scripts.Core.Networking;
using Assets.Scripts.Core.Networking.Udp;
using NUnit.Framework;
using System;

public class NetworkingTest
{
    [Test]
    public void Connect()
    {
        var context = new Contexts();

        var messageFactory = context.dependencies.Provide<MessageFactory>();

        var serializerService = context.dependencies.Provide<SerializerService>();

        using (var clientSender = new UdpSender(serializerService, new SendConfiguration
        {
            RemoteHost = "localhost",
            RemotePort = 32100
        }))
        {
            using (var serverListener = new UdpListener(serializerService, new ListenConfiguration
            {
                ListeningPort = 32100,
                ReceiveInterval = 10
            }))
            {
                bool isConnected = false;

                int tryCount = 100;

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
                    && tryCount > 0)
                {
                    clientSender.Send(connectMsg);

                    tryCount--;

                    System.Threading.Thread.Sleep(10);
                }

                Assert.IsTrue(isConnected);
            }
        }
    }
}