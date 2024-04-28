using System.Collections;
using UnityEngine;

namespace Game.GameCamera
{
    public class CameraController : PausableMonoBehavior
    {
        public static CameraController Instance;
        
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform cameraHolderTransform;
        [SerializeField] private Transform cameraTransform;

        [Header("Camera Shake Variables")] 
        [SerializeField] private float defaultShakeDuration;
        [SerializeField] private float shakeIntensity;
        private Coroutine shakeCameraRoutine;
        
        private Vector3 _cameraPosition;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            _cameraPosition = cameraHolderTransform.position;
            _cameraPosition.x = playerTransform.position.x;
            cameraHolderTransform.position = _cameraPosition;
        }

        public void ShakeCamera(float duration = 0.0f)
        {
            if (shakeCameraRoutine != null)
            {
                StopCoroutine(shakeCameraRoutine);
            }

            StartCoroutine(ShakeCameraRoutine(duration > 0.0f ? duration : defaultShakeDuration));

        }

        private IEnumerator ShakeCameraRoutine(float duration)
        {
            var originalPos = cameraTransform.localPosition;
            while (duration > 0.0f)
            {
                if (!Paused)
                {
                    cameraTransform.localPosition = originalPos + Random.insideUnitSphere * shakeIntensity;
                    duration -= Time.deltaTime;
                }
                yield return null;
            }
            cameraTransform.localPosition = originalPos;
        }
    }
}
