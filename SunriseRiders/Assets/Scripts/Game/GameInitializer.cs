using System.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private int delay;
        [SerializeField] private int delayTimeout;
        public void Awake()
        {
           Initialize();
        }

        private async void Initialize()
        {
            await AwaitPlayerSetup();
        }

        private async Task AwaitPlayerSetup()
        {
            Debug.Log("Waiting PlayerSetUp");

            int timeout = delayTimeout;
            while (GameManager.PlayerReference == null && timeout > 0)
            {
                await Task.Delay(delay);
                timeout -= delay;
            }

            Debug.Log(GameManager.PlayerReference != null ? "Succeeded PlayerSetUp" : "Failed PlayerSetup");
            GameManager.PlayerReference.playerInput.UnlockPlayerInput();
        }
    }
}
