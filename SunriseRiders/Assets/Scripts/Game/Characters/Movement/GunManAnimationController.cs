using UnityEngine;

namespace Game.Characters.Movement
{
    public class GunManAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Facing facing = Facing.Right;
        
        private Transform _enemyTransform;
        private Transform _playerTransform;

        private void Start()
        {
            _enemyTransform = transform;
            _playerTransform = EnemyManager.Instance.PlayerTransform;
        }
        
        private void Update()
        {
            if (_playerTransform.position.x < _enemyTransform.position.x && facing == Facing.Right)
            {
                facing = Facing.Left;
               _enemyTransform.eulerAngles += Vector3.up * 180;
            }
            
            if (_playerTransform.position.x > _enemyTransform.position.x && facing == Facing.Left)
            {
                facing = Facing.Right;
                _enemyTransform.eulerAngles += Vector3.up * 180;
            }
        }
        
    }
}
