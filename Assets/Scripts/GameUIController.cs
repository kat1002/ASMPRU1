using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [Header("InGame UI")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Settings Panel")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button settingPanelRestartButton;

    [Header("GameOver Panel")]
    [SerializeField] private GameObject gameOverPanel;

    private void OnEnable()
    {
        AddEvents();
    }

    private void OnDisable()
    {
        RemoveEvents();
    }
    private void AddEvents()
    {
        GameplayEvents.OnUpdateScore.RemoveListener(UpdateScoreText);
        GameplayEvents.OnGameOver.RemoveListener(OpenGameOverPanel);

        GameplayEvents.OnUpdateScore.AddListener(UpdateScoreText);
        GameplayEvents.OnGameOver.AddListener(OpenGameOverPanel);
    }

    private void RemoveEvents()
    {
        GameplayEvents.OnUpdateScore.RemoveListener(UpdateScoreText);
        GameplayEvents.OnUpdateScore.AddListener(UpdateScoreText);
    }

    #region Pause Panel

    public void OpenPausePanel()
    {
        GameplayEvents.OnGamePause?.Invoke();
        settingsPanel.SetActive(true);
    }

    public void ClosePausePanel()
    {
        settingsPanel.SetActive(false);
        GameplayEvents.OnGameUnPause?.Invoke();
    }


    #endregion

    #region GameOver Panel

    public void OpenGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    #endregion

    #region Function

    public void RestartGame()
    {
        SceneLoader.ReloadScene();
    }

    #endregion

    public void UpdateScoreText(int score)
    {
        scoreText.text = $" Score: {score}";
    }


}
