using Assets.Scripts.EntitasExtensions;
using Assets.Scripts.Features.Client.Move;
using Assets.Scripts.Features.Client.Networking;

using UnityEngine;

namespace Assets.Scripts.Features.Client
{
    public class ClientFeaturesBehavaiour:MonoBehaviour
    {
        private FeaturesContainer
            _feature;

        private void Awake()
        {
            var context = Contexts.sharedInstance;

            _feature = context.feautures;

            _feature.Add(new ClientConnectSystem());
            _feature.Add(new ClientSystem());
            _feature.Add(new MoveSystem());
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
