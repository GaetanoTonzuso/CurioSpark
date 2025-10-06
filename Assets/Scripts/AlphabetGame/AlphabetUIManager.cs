using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AlphabetUIManager : MonoBehaviour
{
    [Header("UI Setting")]
    [SerializeField] private TextMeshProUGUI[] _togglesText;
    [SerializeField] private TextMeshProUGUI _letterCovered;
    [SerializeField] private TextMeshProUGUI _textFeedback;

    [Header("Panels Settings")]
    [SerializeField] private GameObject _endScreenPanel;
    

    private int _correctIndex;

    private void OnEnable()
    {
        EventService.Instance.OnLetterAssigned.AddListener(UpdateToggles);
        EventService.Instance.OnGameOver.AddListener(EndScreen);
    }

    private void OnDisable()
    {
        EventService.Instance.OnLetterAssigned.RemoveListener(UpdateToggles);
        EventService.Instance.OnGameOver.RemoveListener(EndScreen);
    }

    private void UpdateToggles(List<string> letters, int number)
    {
        _textFeedback.gameObject.SetActive(false);
        _letterCovered.gameObject.SetActive(false);

        // Defensive check: not enough unique letters
        if (letters.Count < _togglesText.Length)
        {
            Debug.LogWarning("Not enough unique letters to fill all toggles without duplicates.");
            return;
        }

        // Step 1: Create a working copy of the letters list

        List<string> pool = new List<string>(letters);

        // Step 2: Pick a random index for the correct answer
        _correctIndex = Random.Range(0, _togglesText.Length);

        // Step 3: Assign the correct letter
        _togglesText[_correctIndex].text = letters[number];
        _letterCovered.text = letters[number];
        pool.Remove(letters[number]); // remove so it can't be reused

        // Step 4: Fill the rest with unique random letters
        for (int i = 0; i < _togglesText.Length; i++)
        {
            if (i == _correctIndex) continue; // skip the correct one

            int randomIndex = Random.Range(0, pool.Count);
            _togglesText[i].text = pool[randomIndex];
            pool.RemoveAt(randomIndex); // ensure uniqueness
        }  
    }

    public void CheckAnswer(int number)
    {
        TextMeshProUGUI textToggle = _togglesText[number].GetComponentInChildren<TextMeshProUGUI>();
        if (textToggle != null)
        {
            if(textToggle.text == _letterCovered.text)
            {
                //Correct
                _textFeedback.gameObject.SetActive(true);
                _textFeedback.text = "Correct!";

                if(AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySfxClip(0);
                }

                EventService.Instance.OnUpdateScore.InvokeEvent();
            }

            else
            {
                _textFeedback.gameObject.SetActive(true);
                _textFeedback.text = "Try Again!";
            }
        }

        _letterCovered.gameObject.SetActive(true);

        //Update the Alphabet GameManager
        EventService.Instance.OnGenerateNewQuestion.InvokeEvent();

    }  

    private void EndScreen()
    {
        _endScreenPanel.SetActive(true);
    }
}
