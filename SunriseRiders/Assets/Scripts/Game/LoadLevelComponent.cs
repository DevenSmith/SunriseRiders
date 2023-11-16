using System.Collections;
using Devens;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class LoadLevelComponent : MonoBehaviour
    {
        [SerializeField] private StringSO levelNameStringSo;

        private Coroutine loadSceneAsyncRoutine = null;
        
        [UsedImplicitly]
        public void LoadLevel()
        {
            if (loadSceneAsyncRoutine == null)
            {
                loadSceneAsyncRoutine = StartCoroutine(LoadAsyncScene());
            }
        }
        
        private IEnumerator LoadAsyncScene()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelNameStringSo.Value);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}
