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

        // Update is called once per frame
        void Update()
        {
            _bulletTransform.position += _bulletTransform.right * bulletMovementSpeed.Value * Time.deltaTime;
        }
    }
}
