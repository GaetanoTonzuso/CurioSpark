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

    public void QuitGame()
    {
        Application.Quit(); 
    }

    private void UpdateLastScore()
    {
        if (_lastScore != null)
        _lastScore.text = "Last Score: " + PlayerPrefs.GetInt("LastScore");
    }

}
