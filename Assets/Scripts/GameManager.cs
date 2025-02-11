using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int Score { get; private set; }
    [SerializeField] private AsteroidSpawner spawner;

    [SerializeField] private SpriteRenderer bgSpriteRenderer;
    [SerializeField] private Sprite[] bg;

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
        bgSpriteRenderer.sprite = bg[Random.Range(0, bg.Length)];
        ResumeGame();
        Score = 0;
        AddScore(0);
    }

    #region Game Events
    private void AddEvents()
    {
        GameplayEvents.OnEarnScore.RemoveListener(AddScore);
        GameplayEvents.OnGamePause.RemoveListener(PauseGame);
        GameplayEvents.OnGameUnPause.RemoveListener(ResumeGame);
        GameplayEvents.OnGameOver.RemoveListener(PauseGame);

        GameplayEvents.OnEarnScore.AddListener(AddScore);
        GameplayEvents.OnGamePause.AddListener(PauseGame);
        GameplayEvents.OnGameUnPause.AddListener(ResumeGame);
        GameplayEvents.OnGameOver.AddListener(PauseGame);
    }

    private void RemoveEvents()
    {
        GameplayEvents.OnEarnScore.RemoveListener(AddScore);
        GameplayEvents.OnGamePause.RemoveListener(PauseGame);
        GameplayEvents.OnGameUnPause.RemoveListener(ResumeGame);
        GameplayEvents.OnGameOver.RemoveListener(PauseGame);
    }
    #endregion
    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void AddScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        GameplayEvents.OnUpdateScore?.Invoke(Score);

        if (Score % 10 == 0)
        {
            spawner.SpawnAmount += 1;
        }
    }
}
