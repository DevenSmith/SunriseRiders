using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Game.Characters.Movement
{
    public class DynoManAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Facing facing = Facing.Right;
        
        private Transform _enemyTransform;
        private Transform _playerTransform;
        [SerializeField] private Health.Health health;
        
        private const string DieTrigger = "DIE";
        private const string throwTrigger = "THROW";

        private bool dying = false;
        private bool doneDying = false;
        [SerializeField] private Collider[] colliders;
        [SerializeField] private Rigidbody charRigidbody;

        [Header("DEBUG")]
        [SerializeField] private bool TestThrowAnimation = false;

        private void Awake()
        {
            if (health == null)
            {
                health = GetComponent<Health.Health>();
                if (health == null)
                {
                    Debug.LogError( gameObject.name+ " No Health Component Assigned to " + this.name);
                }
            }

            if (health != null)
            {
                health.onDie.AddListener(Died);
            }
        }

        private void Start()
        {
            _enemyTransform = transform;
            _playerTransform = EnemyManager.Instance.PlayerTransform;
        }
        
        private void OnDestroy()
        {
            if (health != null)
            {
                health.onDie.RemoveListener(Died);
            }
        }

        private void Died()
        {
            dying = true;

            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }

            charRigidbody.isKinematic = true;
            charRigidbody.useGravity = false;

            StartCoroutine(SetRigWeight());
            animator.SetTrigger(DieTrigger);
        }

        private IEnumerator SetRigWeight()
        {
            while (animator != null && animator.IsInTransition(0))
            {
                yield return null;
            }
            yield return new WaitForEndOfFrame();
        }
        
        private void Update()
        {
            if (TestThrowAnimation)
            {
                TestThrowAnimation = false;
                animator.SetTrigger(throwTrigger);
            }
            
            if (dying)
            {
                if (!doneDying && animator.GetCurrentAnimatorStateInfo(0).IsName("Done"))
                {
                    doneDying = true;
                    gameObject.SetActive(false);
                }
            }
            else
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
}
