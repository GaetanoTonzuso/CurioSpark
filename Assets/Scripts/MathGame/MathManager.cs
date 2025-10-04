using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using System.Collections;
using Unity.VisualScripting;

public class MathManager : MonoBehaviour
{
    [Header("Numbers UI")]
    [SerializeField] private TextMeshProUGUI _firstNumber;
    [SerializeField] private TextMeshProUGUI _secondNumber;

    [Header("Numbers Settings")]
    [SerializeField] private int _minNumb = 0;
    [Range(0,50)]
    [SerializeField] private int _maxNumb;

    [Header("Timer")]
    [SerializeField] private float _maxTimer;
    [SerializeField] private TextMeshProUGUI _timerText;

    //Variables not serialized
    private int _itemsToSpawn;
    private int _result;
    private int _score;
    public int Score { get { return _score; } }

    private List<int> _numbers = new List<int>();
    private float _currentTimer;
    private bool _isGameOver;


    private Coroutine _timerRoutine;
    private Coroutine _generateQuestionRoutine;

    private void OnEnable()
    {
        EventService.Instance.OnUpdateScore.AddListener(UpdateScore);
        EventService.Instance.OnGenerateNewQuestion.AddListener(StartNewQuestion);
        EventService.Instance.OnPauseGame.AddListener(PauseGame);
        EventService.Instance.OnUnpauseGame.AddListener(UnpauseGame);
    }

    private void OnDisable()
    {
        EventService.Instance.OnUpdateScore.RemoveListener(UpdateScore);
        EventService.Instance.OnGenerateNewQuestion.RemoveListener(StartNewQuestion);
        EventService.Instance.OnPauseGame.RemoveListener(PauseGame);
        EventService.Instance.OnUnpauseGame.AddListener(UnpauseGame);
    }


    private void Start()
    {
        _itemsToSpawn = GetComponent<GridSpawner>().ItemsToSpawn;

        if(_itemsToSpawn != 0)
        {
            GenerateQuestion();
            AssignNumbers();
        }

        _currentTimer = _maxTimer;

        if(_timerRoutine == null)
        {
            _timerRoutine = StartCoroutine(StartTimer());
        }

        AudioManager.Instance.PlayAmbience(1);
    }

    private void GenerateQuestion()
    {
         int _firstN;
         int _secondN;
  
        _firstN = Random.Range(_minNumb, _maxNumb);
        _firstNumber.text = _firstN.ToString();
        _secondN = Random.Range(_minNumb, _maxNumb);
        _secondNumber.text = _secondN.ToString();
        _result = _firstN + _secondN;

        //Send Communication to DragBarrel to set required number
        EventService.Instance.OnSetAnswer.InvokeEvent(_result);
    }

    private void AssignNumbers()
    {
        while(_numbers.Count <= _itemsToSpawn)
        {
            int randomNum = Random.Range(0, _result + 10);
            if (!_numbers.Contains(randomNum))
            {
                _numbers.Add(randomNum);
            }
        }

        if (!_numbers.Contains(_result))
        {
            _numbers[Random.Range(0,_numbers.Count)] = _result;
        }

        //Send Communication to GridSpawner to start spawning barrels based on numbers
        EventService.Instance.OnSpawnItems.InvokeEvent(_numbers);
    }

    public void UpdateScore()
    {
        _score ++;
        AudioManager.Instance.PlaySfxClip(0);
    }

    public void StartNewQuestion()
    {
        if(_generateQuestionRoutine == null)
        {
            _generateQuestionRoutine = StartCoroutine(GenerateQuestionRoutine());
        }
    }

    private IEnumerator StartTimer()
    {
        while (!IsGameOver())
        {
            _currentTimer -= Time.deltaTime;
            if (_currentTimer < 0) _currentTimer = 0;

            _timerText.text = Mathf.CeilToInt(_currentTimer).ToString();
            yield return null;
        }

        // End Screen
        EventService.Instance.OnEndGame.InvokeEvent(_score);

        //Save LastScore
        PlayerPrefs.SetInt("LastScore", _score);
        PlayerPrefs.Save();

        _timerRoutine = null;
    }

    public bool IsGameOver()
    {
        _isGameOver = _currentTimer <= 0;
        return _isGameOver;
    }


    private IEnumerator GenerateQuestionRoutine()
    {
        yield return new WaitForSeconds(2f);
        GenerateQuestion();
        AssignNumbers();
        EventService.Instance.OnUpdateFeedback.InvokeEvent("");
        _generateQuestionRoutine = null;
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void UnpauseGame()
    {
        Time.timeScale = 1f;
    }
}

