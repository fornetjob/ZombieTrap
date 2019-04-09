//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity roomEntity { get { return GetGroup(GameMatcher.Room).GetSingleEntity(); } }
    public Assets.Scripts.Features.Room.RoomComponent room { get { return roomEntity.room; } }
    public bool hasRoom { get { return roomEntity != null; } }

    public GameEntity SetRoom(uint newNumber) {
        if (hasRoom) {
            throw new Entitas.EntitasException("Could not set Room!\n" + this + " already has an entity with Assets.Scripts.Features.Room.RoomComponent!",
                "You should check if the context already has a roomEntity before setting it or use context.ReplaceRoom().");
        }
        var entity = CreateEntity();
        entity.AddRoom(newNumber);
        return entity;
    }

    public void ReplaceRoom(uint newNumber) {
        var entity = roomEntity;
        if (entity == null) {
            entity = SetRoom(newNumber);
        } else {
            entity.ReplaceRoom(newNumber);
        }
    }

    public void RemoveRoom() {
        roomEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Assets.Scripts.Features.Room.RoomComponent room { get { return (Assets.Scripts.Features.Room.RoomComponent)GetComponent(GameComponentsLookup.Room); } }
    public bool hasRoom { get { return HasComponent(GameComponentsLookup.Room); } }

    public void AddRoom(uint newNumber) {
        var index = GameComponentsLookup.Room;
        var component = (Assets.Scripts.Features.Room.RoomComponent)CreateComponent(index, typeof(Assets.Scripts.Features.Room.RoomComponent));
        component.number = newNumber;
        AddComponent(index, component);
    }

    public void ReplaceRoom(uint newNumber) {
        var index = GameComponentsLookup.Room;
        var component = (Assets.Scripts.Features.Room.RoomComponent)CreateComponent(index, typeof(Assets.Scripts.Features.Room.RoomComponent));
        component.number = newNumber;
        ReplaceComponent(index, component);
    }

    public void RemoveRoom() {
        RemoveComponent(GameComponentsLookup.Room);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherRoom;

    public static Entitas.IMatcher<GameEntity> Room {
        get {
            if (_matcherRoom == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Room);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherRoom = matcher;
            }

            return _matcherRoom;
        }
    }
}