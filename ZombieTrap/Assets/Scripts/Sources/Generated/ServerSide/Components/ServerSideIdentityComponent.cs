//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ServerSideEntity {

    public Assets.Scripts.Features.Core.Identity.IdentityComponent identity { get { return (Assets.Scripts.Features.Core.Identity.IdentityComponent)GetComponent(ServerSideComponentsLookup.Identity); } }
    public bool hasIdentity { get { return HasComponent(ServerSideComponentsLookup.Identity); } }

    public void AddIdentity(ulong newValue) {
        var index = ServerSideComponentsLookup.Identity;
        var component = (Assets.Scripts.Features.Core.Identity.IdentityComponent)CreateComponent(index, typeof(Assets.Scripts.Features.Core.Identity.IdentityComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceIdentity(ulong newValue) {
        var index = ServerSideComponentsLookup.Identity;
        var component = (Assets.Scripts.Features.Core.Identity.IdentityComponent)CreateComponent(index, typeof(Assets.Scripts.Features.Core.Identity.IdentityComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveIdentity() {
        RemoveComponent(ServerSideComponentsLookup.Identity);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiInterfaceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ServerSideEntity : IIdentityEntity { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class ServerSideMatcher {

    static Entitas.IMatcher<ServerSideEntity> _matcherIdentity;

    public static Entitas.IMatcher<ServerSideEntity> Identity {
        get {
            if (_matcherIdentity == null) {
                var matcher = (Entitas.Matcher<ServerSideEntity>)Entitas.Matcher<ServerSideEntity>.AllOf(ServerSideComponentsLookup.Identity);
                matcher.componentNames = ServerSideComponentsLookup.componentNames;
                _matcherIdentity = matcher;
            }

            return _matcherIdentity;
        }
    }
}
