using Assets.Scripts.EntitasExtensions;
using Assets.Scripts.Features.Server.Networking;

using UnityEngine;

namespace Assets.Scripts.Features.Server
{
    public class ServerFeaturesBehaviour:MonoBehaviour
    {
        private FeaturesContainer
            _feature;

        private void Awake()
        {
            var context = Contexts.sharedInstance;

            _feature = context.feautures;

            _feature.Add(new ServerSideEventSystems(context));
            _feature.Add(new ServerSystem());
        }

        private void Start()
        {
            _feature.Initialize();
        }

        private void Update()
        {
            _feature.Execute();
        }
    }
}
