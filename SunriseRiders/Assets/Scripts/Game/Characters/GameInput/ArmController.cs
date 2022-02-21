using UnityEngine;

namespace Game.Characters.GameInput
{
    /// <summary>
    /// for now it follows the mouse later consider having a second joystick to control arm direction.
    /// </summary>
    public class ArmController : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Transform armTransform;
        public void Start()
        {
            if (playerInput == null)
            {
                playerInput = GetComponent<PlayerInput>();
                if (playerInput == null)
                {
                    Debug.LogError(gameObject.name + " armController doesn't have a player input");
                }
            }

            if (armTransform == null)
                armTransform = transform;
        }

        public void Update()
        {
            armTransform.rotation = Quaternion.FromToRotation(Vector3.right, playerInput.ArmDirection);
        }
    }
}
