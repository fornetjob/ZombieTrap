using UnityEngine;

namespace Assets.Scripts.Features.Core.Sprites
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteView:ViewBase
    {
        private SpriteRenderer
            _rend;

        protected override void OnEntityAttach(GameEntity entity)
        {
            if (_rend == null)
            {
                _rend = gameObject.GetComponent<SpriteRenderer>();
            }

            _rend.sprite = entity.sprite.value;
            
            if (entity.hasSpriteSize)
            {
                _rend.drawMode = SpriteDrawMode.Sliced;
                _rend.size = entity.spriteSize.value;
            }
        }
    }
}
