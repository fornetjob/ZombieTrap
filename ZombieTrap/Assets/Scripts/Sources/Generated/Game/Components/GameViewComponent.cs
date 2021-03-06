//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Assets.Scripts.Features.Core.Views.ViewComponent view { get { return (Assets.Scripts.Features.Core.Views.ViewComponent)GetComponent(GameComponentsLookup.View); } }
    public bool hasView { get { return HasComponent(GameComponentsLookup.View); } }

    public void AddView(string newName, GameEntity newAttachedEntity) {
        var index = GameComponentsLookup.View;
        var component = (Assets.Scripts.Features.Core.Views.ViewComponent)CreateComponent(index, typeof(Assets.Scripts.Features.Core.Views.ViewComponent));
        component.name = newName;
        component.attachedEntity = newAttachedEntity;
        AddComponent(index, component);
    }

    public void ReplaceView(string newName, GameEntity newAttachedEntity) {
        var index = GameComponentsLookup.View;
        var component = (Assets.Scripts.Features.Core.Views.ViewComponent)CreateComponent(index, typeof(Assets.Scripts.Features.Core.Views.ViewComponent));
        component.name = newName;
        component.attachedEntity = newAttachedEntity;
        ReplaceComponent(index, component);
    }

    public void RemoveView() {
        RemoveComponent(GameComponentsLookup.View);
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

    static Entitas.IMatcher<GameEntity> _matcherView;

    public static Entitas.IMatcher<GameEntity> View {
        get {
            if (_matcherView == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.View);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherView = matcher;
            }

            return _matcherView;
        }
    }
}
