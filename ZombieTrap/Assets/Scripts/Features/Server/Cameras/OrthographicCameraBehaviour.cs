using UnityEngine;

namespace Assets.Scripts.Features.Server.Cameras
{
    public class OrthographicCameraBehaviour:MonoBehaviour
    {
        private Camera
            _camera;

        private float
            _aspect;

        private void Awake()
        {
            _camera = gameObject.GetComponent<Camera>();

            ResizeCamera();
        }

        private void Update()
        {
            if (_aspect != _camera.aspect)
            {
                ResizeCamera();
            }
        }

        private void ResizeCamera()
        {
            var size = Vector2.one * 20;

            size[0] /= _camera.aspect;

            size /= 2f;

            _camera.orthographicSize = Mathf.Max(size.x, size.y);

            _aspect = _camera.aspect;
        }
    }
}
