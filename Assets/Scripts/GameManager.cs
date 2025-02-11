using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int Score { get; private set; }

    private void OnEnable()
    {
        AddEvents();
    }

    private void OnDisable()
    {
        RemoveEvents();
    }

    private void Start()
    {
        Pause();
        Score = 0;
        AddScore(0);
    }

    #region Game Events
    private void AddEvents()
    {
        GameplayEvents.OnEarnScore.RemoveListener(AddScore);
        GameplayEvents.OnGamePause.RemoveListener(PauseGame);
        GameplayEvents.OnGameUnPause.RemoveListener(Pause);
        GameplayEvents.OnGameOver.RemoveListener(PauseGame);

        GameplayEvents.OnEarnScore.AddListener(AddScore);
        GameplayEvents.OnGamePause.AddListener(PauseGame);
        GameplayEvents.OnGameUnPause.AddListener(Pause);
        GameplayEvents.OnGameOver.AddListener(PauseGame);
    }

    private void RemoveEvents()
    {
        GameplayEvents.OnEarnScore.RemoveListener(AddScore);
        GameplayEvents.OnGamePause.RemoveListener(PauseGame);
        GameplayEvents.OnGameUnPause.RemoveListener(Pause);
        GameplayEvents.OnGameOver.RemoveListener(PauseGame);
    }
    #endregion
    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void Pause()
    {
        Time.timeScale = 1f;
    }

    public void AddScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        GameplayEvents.OnUpdateScore?.Invoke(Score);
    }
}
