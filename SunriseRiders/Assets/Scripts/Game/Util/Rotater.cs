using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Util
{
    public class Rotater : PausableMonoBehavior
    {
        [Serializable]
        public struct Rotation
        {
            public Vector3 angle;
            public float speed;
        }

        [SerializeField] private List<Rotation> Rotations;

        private void Update()
        {
            if (Paused)
                return;

            foreach (var rotation in Rotations)
            {
                transform.eulerAngles += rotation.angle * rotation.speed * Time.deltaTime;
            }
        }
    }
}
