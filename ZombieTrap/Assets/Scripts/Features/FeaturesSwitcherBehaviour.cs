using UnityEngine;

using System.Linq;
using Assets.Scripts.Features.Client.Networking;
using Assets.Scripts.Features.Core.Views;
using Assets.Scripts.Features.Core.GameTime;
using Assets.Scripts.EntitasExtensions;

namespace Assets.Scripts.Features
{
    public class FeaturesSwitcherBehaviour:MonoBehaviour
    {
        private FeaturesContainer
            _feature;

        private void Awake()
        {
            var context = Contexts.sharedInstance;

            _feature = new FeaturesContainer(context);

            _feature.Add(new GameEventSystems(context));
            _feature.Add(new GameTimeSystem());
            _feature.Add(new ViewSystem());

            var args = System.Environment.GetCommandLineArgs();

            if (args.Contains("server"))
            {
                AddServerFeatures(context);
            }
            else
            {
                AddClientFeatures(context);
            }
        }

        private void AddClientFeatures(Contexts context)
        {
            _feature.Add(new ClientReceiveSystem());
        }

        private void AddServerFeatures(Contexts context)
        {

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
