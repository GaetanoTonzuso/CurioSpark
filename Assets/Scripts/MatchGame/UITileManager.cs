using TMPro;
using UnityEngine;

public class UITileManager : MonoBehaviour
{
    [Header("Game Screen")]
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void OnEnable()
    {
        EventService.Instance.OnEndGame.AddListener(WinScreen);
        EventService.Instance.OnGameOver.AddListener(GameOverScreen);
    }

    private void OnDisable()
    {
        EventService.Instance.OnEndGame.RemoveListener(WinScreen);
        EventService.Instance.OnGameOver.RemoveListener(GameOverScreen);
    }

    public void WinScreen(int finalScore)
    {
        _scoreText.text = "Score: " + finalScore;
        _winScreen.SetActive(true);
    }

    public void GameOverScreen()
    {
        _gameOverScreen.SetActive(true);
    }
}
