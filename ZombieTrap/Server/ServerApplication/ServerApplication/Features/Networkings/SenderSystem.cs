public class SenderSystem : IFixedExecuteSystem
{
    private PlayersPooling _players = null;
    private MessagePooling _messagePooling = null;

    public void FixedExecute()
    {
        var players = _players.GetPlayers();

        for (int i = 0; i < players.Count; i++)
        {
            var player = players[i];

            var strongMessageQueue = _messagePooling.GetStrongMessageQueue(player.PlayerId);

            if (strongMessageQueue.Count > 0)
            {
                player.Sender.Send(strongMessageQueue.Peek());
            }

            var messageQueue = _messagePooling.GetMessageQueue(player.PlayerId);

            while (messageQueue.Count > 0)
            {
                player.Sender.Send(messageQueue.Dequeue());
            }
        }
    }
}