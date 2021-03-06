//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Assets.Scripts.Features.Projectiles.ProjectileComponent projectile { get { return (Assets.Scripts.Features.Projectiles.ProjectileComponent)GetComponent(GameComponentsLookup.Projectile); } }
    public bool hasProjectile { get { return HasComponent(GameComponentsLookup.Projectile); } }

    public void AddProjectile(float newDuration, UnityEngine.Vector3 newPosFrom, UnityEngine.Vector3 newPosTo) {
        var index = GameComponentsLookup.Projectile;
        var component = (Assets.Scripts.Features.Projectiles.ProjectileComponent)CreateComponent(index, typeof(Assets.Scripts.Features.Projectiles.ProjectileComponent));
        component.duration = newDuration;
        component.posFrom = newPosFrom;
        component.posTo = newPosTo;
        AddComponent(index, component);
    }

    public void ReplaceProjectile(float newDuration, UnityEngine.Vector3 newPosFrom, UnityEngine.Vector3 newPosTo) {
        var index = GameComponentsLookup.Projectile;
        var component = (Assets.Scripts.Features.Projectiles.ProjectileComponent)CreateComponent(index, typeof(Assets.Scripts.Features.Projectiles.ProjectileComponent));
        component.duration = newDuration;
        component.posFrom = newPosFrom;
        component.posTo = newPosTo;
        ReplaceComponent(index, component);
    }

    public void RemoveProjectile() {
        RemoveComponent(GameComponentsLookup.Projectile);
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

    static Entitas.IMatcher<GameEntity> _matcherProjectile;

    public static Entitas.IMatcher<GameEntity> Projectile {
        get {
            if (_matcherProjectile == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Projectile);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherProjectile = matcher;
            }

            return _matcherProjectile;
        }
    }
}
