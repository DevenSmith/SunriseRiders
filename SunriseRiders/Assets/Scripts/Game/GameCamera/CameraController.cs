using UnityEngine;

namespace Game.GameCamera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;

        private Transform _cameraTransform;
        private Vector3 _cameraPosition;
        private void Awake()
        {
            _cameraTransform = transform;
        }

        // Update is called once per frame
        void Update()
        {
            _cameraPosition = _cameraTransform.position;
            _cameraPosition.x = playerTransform.position.x;
            _cameraTransform.position = _cameraPosition;
        }
    }
}
