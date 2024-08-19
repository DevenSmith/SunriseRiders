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
            if (SceneTransition.Instance != null)
            {
                SceneTransition.Instance.onTransitionFinished.AddListener(ImmediateLoadLevel);
                SceneTransition.Instance.StartTransitionOut();
            }
            else
            {
                ImmediateLoadLevel();
            }
        }

        private void ImmediateLoadLevel()
        {
            if (SceneTransition.Instance != null)
            {
                SceneTransition.Instance.onTransitionFinished.AddListener(ImmediateLoadLevel);
            }

            if (loadSceneAsyncRoutine == null)
            {
                if (SceneTransition.Instance != null)
                {
                    loadSceneAsyncRoutine = SceneTransition.Instance.StartCoroutine(LoadAsyncScene());
                }
                else
                {
                    loadSceneAsyncRoutine = StartCoroutine(LoadAsyncScene());
                }
                
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
