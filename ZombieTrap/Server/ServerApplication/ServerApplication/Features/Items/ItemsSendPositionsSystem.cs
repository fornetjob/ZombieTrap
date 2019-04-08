using Game.Core.Networking;

public class ItemsSendPositionsSystem : IFixedExecuteSystem
{
    #region Factories

    private MessageFactory _messageFactory = null;

    #endregion

    #region Poolings

    private RoomsPooling _roomsPooling = null;

    #endregion

    public void FixedExecute()
    {
        for (int roomIndex = 0; roomIndex < _roomsPooling.Rooms.Count; roomIndex++)
        {
            var room = _roomsPooling.Rooms[roomIndex];

            _messageFactory.CreateMessage(room.RoomId, MessageType.Positions);
        }
    }
}