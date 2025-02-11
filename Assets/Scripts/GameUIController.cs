using TMPro;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [Header("Menu UI")]
    [SerializeField] private GameObject instructionPanel;

    [Header("InGame UI")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Settings Panel")]
    [SerializeField] private GameObject settingsPanel;

    [Header("GameOver Panel")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI finalScore;

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

    #region Instruction Panel

    public void OpenInstructionPanel()
    {
        instructionPanel.SetActive(true);
    }

    public void CloseInstructionPanel()
    {
        instructionPanel.SetActive(false);
    }

    #endregion

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
        finalScore.text = scoreText.text;
    }

    #endregion

    #region Function

    public void StartGame()
    {
        SceneLoader.LoadScene("GameScene");
    }

    public void RestartGame()
    {
        SceneLoader.ReloadScene();
    }

    public void QuitGame()
    {
        SceneLoader.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion

    public void UpdateScoreText(int score)
    {
        scoreText.text = $" Score: {score}";
    }


}
