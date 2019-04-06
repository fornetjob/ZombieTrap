using Assets.Scripts.EntitasExtensions;
using Assets.Scripts.Features.Server.Networking;
using Assets.Scripts.Features.Server.Room;
using Assets.Scripts.Features.Server.Zombies;
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
            _feature.Add(new ZombiesMoveSystem());
            _feature.Add(new RoomSystem());
        }

        private void Start()
        {
            _feature.Initialize();
        }

        private void Update()
        {
            _feature.Execute();
        }

        private void FixedUpdate()
        {
            _feature.FixedExecute();
        }
    }
}
