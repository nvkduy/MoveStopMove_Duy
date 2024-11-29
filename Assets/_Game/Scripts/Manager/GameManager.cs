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
                Time.timeScale = 0;
                StartCoroutine(ResumeTime(3f));
                break;
            case GameState.Pasue:
                //Time.timeScale = 0;
                break;



        }
    }

   private IEnumerator ResumeTime(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);  // Chờ 3 giây (không bị ảnh hưởng bởi Time.timeScale)

        // Khôi phục thời gian
        Time.timeScale = 1f;
    }
    public bool IsState(GameState gameState)
    {
        return this.currentState == gameState;
    }
}
