//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Assets.Scripts.Features.Zombies.ZombieComponent zombie { get { return (Assets.Scripts.Features.Zombies.ZombieComponent)GetComponent(GameComponentsLookup.Zombie); } }
    public bool hasZombie { get { return HasComponent(GameComponentsLookup.Zombie); } }

    public void AddZombie(Game.Core.ItemType newType, float newRadius) {
        var index = GameComponentsLookup.Zombie;
        var component = (Assets.Scripts.Features.Zombies.ZombieComponent)CreateComponent(index, typeof(Assets.Scripts.Features.Zombies.ZombieComponent));
        component.type = newType;
        component.radius = newRadius;
        AddComponent(index, component);
    }

    public void ReplaceZombie(Game.Core.ItemType newType, float newRadius) {
        var index = GameComponentsLookup.Zombie;
        var component = (Assets.Scripts.Features.Zombies.ZombieComponent)CreateComponent(index, typeof(Assets.Scripts.Features.Zombies.ZombieComponent));
        component.type = newType;
        component.radius = newRadius;
        ReplaceComponent(index, component);
    }

    public void RemoveZombie() {
        RemoveComponent(GameComponentsLookup.Zombie);
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

    static Entitas.IMatcher<GameEntity> _matcherZombie;

    public static Entitas.IMatcher<GameEntity> Zombie {
        get {
            if (_matcherZombie == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Zombie);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherZombie = matcher;
            }

            return _matcherZombie;
        }
    }
}
