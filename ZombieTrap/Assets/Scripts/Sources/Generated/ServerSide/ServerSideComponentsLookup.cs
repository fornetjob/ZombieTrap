//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentLookupGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public static class ServerSideComponentsLookup {

    public const int Bound = 0;
    public const int Identity = 1;
    public const int Player = 2;
    public const int Room = 3;
    public const int RoomIdentity = 4;
    public const int PlayerListener = 5;

    public const int TotalComponents = 6;

    public static readonly string[] componentNames = {
        "Bound",
        "Identity",
        "Player",
        "Room",
        "RoomIdentity",
        "PlayerListener"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Assets.Scripts.Features.Core.Bound.BoundComponent),
        typeof(Assets.Scripts.Features.Core.Identity.IdentityComponent),
        typeof(Assets.Scripts.Features.Server.Networking.PlayerComponent),
        typeof(Assets.Scripts.Features.Server.Room.RoomComponent),
        typeof(Assets.Scripts.Features.Server.Room.RoomIdentity),
        typeof(PlayerListenerComponent)
    };
}