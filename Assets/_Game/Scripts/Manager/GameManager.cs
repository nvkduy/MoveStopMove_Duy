using System.Collections;
using UnityEngine;

public enum GameState
{
    MainMenu,
    GamePlay,
    Pasue

}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private CameraFollower cameraFollower;
    private GameState currentState;


    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.GamePlay:
                break;
            case GameState.Pasue:
                //Time.timeScale = 0;
                break;



        }
    }


    public bool IsState(GameState gameState)
    {
        return this.currentState == gameState;
    }
}
