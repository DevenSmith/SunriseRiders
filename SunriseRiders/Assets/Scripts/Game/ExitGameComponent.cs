using JetBrains.Annotations;
using UnityEngine;

namespace Game
{
    public class ExitGameComponent : MonoBehaviour
    {
        [UsedImplicitly]
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
