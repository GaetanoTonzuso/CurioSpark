using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    [Header("Account Creation UI")]
    [SerializeField] private TMP_InputField _usernameField;
    [SerializeField] private TMP_InputField _passwordField;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _loginPage;
    [SerializeField] private GameObject _createProfilePage;

    private int _profilesCount;

    private void Start()
    {
        _profilesCount = PlayerPrefs.GetInt("ProfilesCount", 0);
        GetProfiles();
    }

    public void CreateProfile()
    {
        string username = _usernameField.text;
        string password = _passwordField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Debug.LogWarning("Username o password non possono essere vuoti!");
            return;
        }

        int index = _profilesCount;

        PlayerPrefs.SetString($"Profile_{index}_Name", username);
        PlayerPrefs.SetString($"Profile_{index}_Password", password);

        _profilesCount++;
        PlayerPrefs.SetInt("ProfilesCount", _profilesCount);
        PlayerPrefs.Save();

        _text.text = "Profile Created, go back to Login Page!";
    }

    public void GetProfiles()
    {
        for (int i = 0; i < _profilesCount; i++)
        {
            string name = PlayerPrefs.GetString($"Profile_{i}_Name", "Vuoto");
            string password = PlayerPrefs.GetString($"Profile_{i}_Password", "N/A");

            Debug.Log($"Profilo {i}: {name} | Password: {password}");
        }
    }

    public void ClearAllProfiles()
    {
        PlayerPrefs.DeleteAll();
        _profilesCount = 0;
        Debug.Log("Tutti i profili sono stati eliminati.");
    }

    public void ReturnLoginPage()
    {
        _text.text = "";
        _createProfilePage.SetActive(false);
        _loginPage.SetActive(true);
    }
}
