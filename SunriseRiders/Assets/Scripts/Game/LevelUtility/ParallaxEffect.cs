using UnityEngine;

namespace Game.LevelUtility
{
    public class ParallaxEffect : MonoBehaviour
    {
        public Transform cameraTransform; // Reference to the camera's transform
        public float parallaxFactor = 0.5f; // Factor to control the speed of the parallax effect

        private Vector3 _lastCameraPosition;
        private Vector3 _initialPosition;

        void Start()
        {
            if (cameraTransform == null)
            {
                cameraTransform = Camera.main.transform;
            }
        
            _initialPosition = transform.position;
            _lastCameraPosition = cameraTransform.position;
        }

        void FixedUpdate()
        {
            Vector3 cameraMovement = cameraTransform.position - _lastCameraPosition;

            // Update the position of the object based on the camera's movement
            transform.position += cameraMovement * parallaxFactor;

            // Update the last camera position
            _lastCameraPosition = cameraTransform.position;
        }
    }
}
