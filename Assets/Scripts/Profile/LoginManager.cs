using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    [Header("Login UI")]
    [SerializeField] private TMP_InputField _usernameInputField;
    [SerializeField] private TMP_InputField _passwordInputField;
    [SerializeField] private TextMeshProUGUI _textFeedback;
    [SerializeField] private GameObject _loginPage;
    [SerializeField] private GameObject _creationPage;

    private int _profilesCount;


    private void Start()
    {
        if(_textFeedback != null )
        _textFeedback.text = "";

    }

    public void CheckProfile()
    {
        _profilesCount = PlayerPrefs.GetInt("ProfilesCount", 0);

        string username = _usernameInputField.text.Trim();
        string password = _passwordInputField.text.Trim();

        for (int i = 0; i < _profilesCount; i++)
        {
            string savedUsername = PlayerPrefs.GetString($"Profile_{i}_Name");
            string savedPassword = PlayerPrefs.GetString($"Profile_{i}_Password");

            if(savedUsername == username && savedPassword == password)
            {
                Debug.Log("Profile Found");
                _textFeedback.text = "Login Successfull!";
                SceneManager.LoadSceneAsync(1);
                // Next Page Load
                break;
            }

            _usernameInputField.text = "";
            _passwordInputField.text = "";
            _textFeedback.text = "Account not found, try again!";
        }
    }

    public void CreationPage()
    {
        _textFeedback.text = "";
        _usernameInputField.text = "";
        _passwordInputField.text = "";
        _loginPage.SetActive(false);
        _creationPage.SetActive(true);
    }

}
