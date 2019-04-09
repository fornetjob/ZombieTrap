//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Assets.Scripts.Features.Prefabs.AttachedComponent attached { get { return (Assets.Scripts.Features.Prefabs.AttachedComponent)GetComponent(GameComponentsLookup.Attached); } }
    public bool hasAttached { get { return HasComponent(GameComponentsLookup.Attached); } }

    public void AddAttached(Assets.Scripts.Features.Prefabs.ViewComposite newValue) {
        var index = GameComponentsLookup.Attached;
        var component = (Assets.Scripts.Features.Prefabs.AttachedComponent)CreateComponent(index, typeof(Assets.Scripts.Features.Prefabs.AttachedComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAttached(Assets.Scripts.Features.Prefabs.ViewComposite newValue) {
        var index = GameComponentsLookup.Attached;
        var component = (Assets.Scripts.Features.Prefabs.AttachedComponent)CreateComponent(index, typeof(Assets.Scripts.Features.Prefabs.AttachedComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAttached() {
        RemoveComponent(GameComponentsLookup.Attached);
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

    static Entitas.IMatcher<GameEntity> _matcherAttached;

    public static Entitas.IMatcher<GameEntity> Attached {
        get {
            if (_matcherAttached == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Attached);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAttached = matcher;
            }

            return _matcherAttached;
        }
    }
}