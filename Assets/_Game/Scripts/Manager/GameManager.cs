using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Gameplay,
    Pasue

}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private CameraFollower cameraFollower;
    private GameState gameState;

    
    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState gameState)
    {
        this.gameState = gameState;
    }

    public bool IsState(GameState gameState)
    {
        return this.gameState == gameState;
    }

}
