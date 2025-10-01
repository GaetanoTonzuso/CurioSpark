using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private GameObject _pauseScreen;

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
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

}
