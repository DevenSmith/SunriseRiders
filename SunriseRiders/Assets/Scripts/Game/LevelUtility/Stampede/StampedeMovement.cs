using System;
using UnityEngine;

namespace Game.LevelUtility.Stampede
{
    public class StampedeMovement : PausableMonoBehavior
    {
        private float speed;

        public void Initialize(float initialSpeed)
        {
            speed = initialSpeed;
        }

        public void Update()
        {
            if (Paused)
                return;
            
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
