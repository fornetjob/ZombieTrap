using UnityEngine;

using System.Linq;
using Assets.Scripts.Features.Client.Networking;
using Assets.Scripts.Features.Core.Views;

namespace Assets.Scripts.Features
{
    public class FeaturesSwitcherBehaviour:MonoBehaviour
    {
        private Feature
            _feature;

        private void Awake()
        {
            var context = Contexts.sharedInstance;

            _feature = new Feature();

            _feature.Add(new ViewSystem(context));

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
            _feature.Add(new ClientReceiveSystem(context));
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
