using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiMathGame : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] TextMeshProUGUI _textScore;

    [Header("Feedback Player")]
    [SerializeField] TextMeshProUGUI _textFeedback;

    [Header("Buttons Pause")]
    [SerializeField] private GameObject[] _pauseButtons;

    [Header("Game End Screen")]
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _endGameScreen;
    [SerializeField] private TextMeshProUGUI _textFinalScore;

    private void Start()
    {
        _textFeedback.text = "";
        _textScore.text = "";
    }

    private void OnEnable()
    {
        EventService.Instance.OnUpdateFeedback.AddListener(UpdateFeedbackText);
        EventService.Instance.OnUpdateScoreUI.AddListener(UpdateScoreText);
        EventService.Instance.OnEndGame.AddListener(GameEndScreen);
        EventService.Instance.OnPauseGame.AddListener(PauseGame);
        EventService.Instance.OnUnpauseGame.AddListener(UnpauseGame);
    }

    private void OnDisable()
    {
        EventService.Instance.OnUpdateFeedback.RemoveListener(UpdateFeedbackText);
        EventService.Instance.OnUpdateScoreUI.RemoveListener(UpdateScoreText);
        EventService.Instance.OnEndGame.RemoveListener(GameEndScreen);
        EventService.Instance.OnPauseGame.RemoveListener(PauseGame);
        EventService.Instance.OnUnpauseGame.RemoveListener(UnpauseGame);
    }

    public void UpdateFeedbackText(string text)
    {
        _textFeedback.text = text;
    }

    public void UpdateScoreText(int score)
    {
        _textScore.text = score.ToString();
    }

    public void GameEndScreen(int finalScore)
    {
        _gameScreen.SetActive(false);
        _textFinalScore.text = "Score: " + finalScore.ToString();
        _endGameScreen.SetActive(true);
    }

    private void PauseGame()
    {
        _gameScreen.SetActive(false);

        foreach(GameObject anim in _pauseButtons)
        {
            if(anim.TryGetComponent<Animator>(out Animator animator))
            {
                animator.enabled = true;
                animator.updateMode = AnimatorUpdateMode.UnscaledTime;

            }
        }
    }

    private void UnpauseGame()
    {
        _gameScreen.SetActive(true);
    }
  
}
