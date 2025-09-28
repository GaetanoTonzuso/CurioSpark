using TMPro;
using UnityEngine;

public class UiMathGame : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] TextMeshProUGUI _textScore;

    [Header("Feedback Player")]
    [SerializeField] TextMeshProUGUI _textFeedback;

    private void Start()
    {
        _textFeedback.text = "";
        _textScore.text = "";
    }

    private void OnEnable()
    {
        EventService.Instance.OnUpdateFeedback.AddListener(UpdateFeedbackText);
        EventService.Instance.OnUpdateScoreUI.AddListener(UpdateScoreText);
    }

    private void OnDisable()
    {
        EventService.Instance.OnUpdateFeedback.RemoveListener(UpdateFeedbackText);
        EventService.Instance.OnUpdateScoreUI.RemoveListener(UpdateScoreText);
    }

    public void UpdateFeedbackText(string text)
    {
        _textFeedback.text = text;
    }

    public void UpdateScoreText(int score)
    {
        _textScore.text = score.ToString();
    }
  
}
