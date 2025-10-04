using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private TextMeshProUGUI _lastScore;

    private void Start()
    {
        UpdateLastScore();
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadSceneAsync(scene);
        Time.timeScale = 1f;
    }

    public void PauseScreen()
    {
        EventService.Instance.OnPauseGame.InvokeEvent();
        _pauseScreen.SetActive(true);
    }

    public void UnpauseScreen()
    {
        EventService.Instance.OnUnpauseGame.InvokeEvent();
        _pauseScreen.SetActive(false);
    }

    private void UpdateLastScore()
    {
        _lastScore.text = "Last Score: " + PlayerPrefs.GetInt("LastScore");
    }

}
