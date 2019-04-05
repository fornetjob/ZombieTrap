//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Assets.Scripts.Features.Client.Networking.ConnectionStateComponent connectionState { get { return (Assets.Scripts.Features.Client.Networking.ConnectionStateComponent)GetComponent(GameComponentsLookup.ConnectionState); } }
    public bool hasConnectionState { get { return HasComponent(GameComponentsLookup.ConnectionState); } }

    public void AddConnectionState(Assets.Scripts.Core.Networking.ConnectionState newValue) {
        var index = GameComponentsLookup.ConnectionState;
        var component = (Assets.Scripts.Features.Client.Networking.ConnectionStateComponent)CreateComponent(index, typeof(Assets.Scripts.Features.Client.Networking.ConnectionStateComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceConnectionState(Assets.Scripts.Core.Networking.ConnectionState newValue) {
        var index = GameComponentsLookup.ConnectionState;
        var component = (Assets.Scripts.Features.Client.Networking.ConnectionStateComponent)CreateComponent(index, typeof(Assets.Scripts.Features.Client.Networking.ConnectionStateComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveConnectionState() {
        RemoveComponent(GameComponentsLookup.ConnectionState);
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

    static Entitas.IMatcher<GameEntity> _matcherConnectionState;

    public static Entitas.IMatcher<GameEntity> ConnectionState {
        get {
            if (_matcherConnectionState == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ConnectionState);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherConnectionState = matcher;
            }

            return _matcherConnectionState;
        }
    }
}
