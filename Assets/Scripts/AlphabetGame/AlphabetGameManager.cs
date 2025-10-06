using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlphabetGameManager : MonoBehaviour
{
    [Header("List Items")]
    [SerializeField] private List<string> _letters = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    [SerializeField] private List<AudioClip> _lettersClips = new List<AudioClip>();

    [Header("Game Settings")]
    [SerializeField] private float _timer = 120f;
    [SerializeField] private TextMeshProUGUI _textTimer;
    [SerializeField] private TextMeshProUGUI _textScore;

    private int _score;
    private int _randomSelect;
    private bool _isGameOver;

    private Coroutine _newRequesRoutine;
    private Coroutine _timerRoutine;

    private void OnEnable()
    {
        EventService.Instance.OnGenerateNewQuestion.AddListener(GenerateNewRequest);
        EventService.Instance.OnPauseGame.AddListener(PauseGame);
        EventService.Instance.OnUnpauseGame.AddListener(UnPauseGame);
        EventService.Instance.OnUpdateScore.AddListener(UpdateScore);
    }

    private void OnDisable()
    {
        EventService.Instance.OnGenerateNewQuestion.RemoveListener(GenerateNewRequest);
        EventService.Instance.OnPauseGame.RemoveListener(PauseGame);
        EventService.Instance.OnUnpauseGame.RemoveListener(UnPauseGame);
        EventService.Instance.OnUpdateScore.RemoveListener(UpdateScore);
    }

    private void Start()
    {
        GenerateNewRequest();

        if(_timerRoutine == null)
        {
            _timerRoutine = StartCoroutine(TimerRoutine());
        }
    }

    private void GenerateNewLetter()
    {
        _randomSelect = Random.Range(0, _letters.Count);
        AudioManager.Instance.PlaySfxClip(_lettersClips[_randomSelect]);
        EventService.Instance.OnLetterAssigned.InvokeEvent(_letters, _randomSelect);
    }

    public void PlaySoundAgain()
    {
        AudioManager.Instance.PlaySfxClip(_lettersClips[_randomSelect]);
    }

    private IEnumerator NewRequest()
    {
        yield return new WaitForSeconds(1f);
        GenerateNewLetter();
        _newRequesRoutine = null;
    }

    private void GenerateNewRequest()
    {
        if(_newRequesRoutine == null && !_isGameOver)
        {
            _newRequesRoutine = StartCoroutine(NewRequest());
        }
    }

    private IEnumerator TimerRoutine()
    {
        while(_timer > 0)
        {
            _timer -= Time.deltaTime;
            _textTimer.text = Mathf.CeilToInt(_timer).ToString();
            yield return null;
        }

        _isGameOver = true;
        EventService.Instance.OnGameOver.InvokeEvent();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void UnPauseGame()
    {
        Time.timeScale = 1f;
    }

    private void UpdateScore()
    {
        _score++;
        _textScore.text = "Score: " + _score.ToString();
    }
}
