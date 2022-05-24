using System;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public enum GameStates
    {
        PLAYING,
        PAUSED
    }

    private static GameStates _currentState;

    public static Action OnGameStateChanged;
    public static GameStates CurrentGameState
    {
        get => _currentState;
        set
        {
            if (value == _currentState)
            {
                return;
            }
            
            _currentState = value;
            OnGameStateChanged?.Invoke();

        }
    }
    
}
