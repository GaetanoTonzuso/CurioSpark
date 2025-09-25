using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

        _profilesCount = PlayerPrefs.GetInt("ProfilesCount", 0);
    }

   /* public void EnterUsername()
    {
        _username = _usernameInputField.text;
    }

    public void EnterPassword()
    {
        _password = _passwordInputField.text;
    }*/

    public void CheckProfile()
    {
        string username = _usernameInputField.text.Trim();
        string password = _passwordInputField.text.Trim();

        Debug.Log("Clicked Login button");

        for (int i = 0; i < _profilesCount; i++)
        {
            string savedUsername = PlayerPrefs.GetString($"Profile_{i}_Name");
            string savedPassword = PlayerPrefs.GetString($"Profile_{i}_Password");

            if(savedUsername == username && savedPassword == password)
            {
                Debug.Log("Profile Found");
                break;
            }
        }
    }

    public void CreationPage()
    {
        _loginPage.SetActive(false);
        _creationPage.SetActive(true);
    }

}
