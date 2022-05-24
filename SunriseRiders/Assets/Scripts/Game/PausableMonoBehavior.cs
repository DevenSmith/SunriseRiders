using System;
using UnityEngine;

namespace Game
{
    public class PausableMonoBehavior : MonoBehaviour
    {
        protected bool Paused = false;
        
        protected virtual void Start()
        {
            GameState.OnGameStateChanged += OnGameStateChanged;
        }

        protected virtual void OnDestroy()
        {
            GameState.OnGameStateChanged -= OnGameStateChanged;
        }
        
        protected virtual void OnGameStateChanged()
        {
           Paused = GameState.CurrentGameState == GameState.GameStates.PAUSED;
        }
        
    }
}
