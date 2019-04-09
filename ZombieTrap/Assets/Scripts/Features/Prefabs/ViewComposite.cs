using UnityEngine;

namespace Assets.Scripts.Features.Prefabs
{
    public class ViewComposite:IEntityAttach
    {
        private ViewBase[] _views;

        public ViewComposite(string path, GameObject root, ViewBase[] views)
        {
            Path = path;
            Root = root;
            _views = views;
        }

        #region Properties

        public readonly string Path;
        public readonly GameObject Root;

        #endregion

        public void AttachEntity(GameEntity entity)
        {
            for (int i = 0; i < _views.Length; i++)
            {
                _views[i].AttachEntity(entity);
            }
        }

        public void DettachEntity(GameEntity entity)
        {
            for (int i = 0; i < _views.Length; i++)
            {
                _views[i].DettachEntity(entity);
            }
        }
    }
}
