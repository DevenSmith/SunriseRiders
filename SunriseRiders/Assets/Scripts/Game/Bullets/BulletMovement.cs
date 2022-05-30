using System;
using Devens;
using UnityEngine;

namespace Game.Bullets
{
    public class BulletMovement : MonoBehaviour
    {
        [SerializeField] private FloatSO bulletMovementSpeed;

        private Transform _bulletTransform;
        private void Awake()
        {
            _bulletTransform = transform;
        }

        private void AdjustZPositionIfNotAtZero()
        {
            if (_bulletTransform.position.z == 0.0f)
            {
                return;
            }
            
            var temp = _bulletTransform.position;
            temp.z = 0.0f;
            _bulletTransform.position = temp;
        }
        

        // Update is called once per frame
        void Update()
        {
            AdjustZPositionIfNotAtZero();
            _bulletTransform.position += _bulletTransform.right * bulletMovementSpeed.Value * Time.deltaTime;
        }
    }
}
