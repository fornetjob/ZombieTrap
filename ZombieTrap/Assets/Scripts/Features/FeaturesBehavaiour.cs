using Assets.Scripts.EntitasExtensions;

using UnityEngine;

namespace Assets.Scripts.Features
{
    public class FeaturesBehavaiour: MonoBehaviour
    {
        private FeaturesContainer
            _feature;

        private void Awake()
        {
            var context = Contexts.sharedInstance;

            _feature = context.feautures;

            _feature.Add(new GameEventSystems(context));
            _feature.Add(new GameTimeSystem());
            _feature.Add(new ViewSystem());

            _feature.Add(new MoveSystem());

            _feature.Add(new ServerConnectionSystem());
            _feature.Add(new ProcessMessagesSystem());
            _feature.Add(new DestroySystem());
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
