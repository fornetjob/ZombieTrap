using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Features.Core.Views
{
    public class ViewSystem:IExecuteSystem
    {
        private IGroup<GameEntity>
            _views;

        private Dictionary<string, ViewBase>
            _dict = new Dictionary<string, ViewBase>();

        public ViewSystem(Contexts context)
        {
            _views = context.game.GetGroup(GameMatcher.View);

            var canvas = GameObject.Find("Canvas");

            foreach (var current in canvas.GetComponentsInChildren<ViewBase>(true))
            {
                _dict.Add(current.gameObject.name, current);
            }
        }

        public void Execute()
        {
            if (_views.count > 0)
            {
                var entities = _views.GetEntities();

                for (int i = 0; i < entities.Length; i++)
                {
                    var entity = entities[i];

                    var view = _dict[entity.view.name];
                    view.AttachEntity(entity.view.attachedEntity);

                    entity.RemoveView();
                }
            }
        }
    }
}
