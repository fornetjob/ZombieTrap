using ServerApplication.Features.Rooms;
using System.Collections.Generic;

public class RoomsPooling : IDependency
{
    public List<Room> Rooms = new List<Room>();
}