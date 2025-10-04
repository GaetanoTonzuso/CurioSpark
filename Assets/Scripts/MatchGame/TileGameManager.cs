using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class TileGameManager : MonoBehaviour
{
    //Variables

    private int _currentReveleadItems;
    public int CurrentRevealedItems { get { return _currentReveleadItems; } }

    private int _currentMatchs = 0;
    private int _matchesWin;
    private int pair = 2;
    private int _score;
    private bool _isGameOver;
    public bool IsGameOver { get { return _isGameOver; } }
    private Coroutine _checkingCoroutine;
    private Coroutine _timerRoutine;

    [Header("Game Settings")]
    [SerializeField] private int _itemsToSpawn = 24;
    [SerializeField] private float _checkingTime = 3f;
    [SerializeField] private float _timer = 300f;
    [SerializeField] private List<TileItem> _namesRevealed = new List<TileItem>();

    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI _textTimer;

    //Variables

    private void OnEnable()
    {
        EventService.Instance.OnUncoverItem.AddListener(OnUncoverItem);
        EventService.Instance.OnPauseGame.AddListener(PauseGame);
        EventService.Instance.OnUnpauseGame.AddListener(UnPauseGame);
    }

    private void OnDisable()
    {
        EventService.Instance.OnUncoverItem.RemoveListener(OnUncoverItem);
        EventService.Instance.OnPauseGame.RemoveListener(PauseGame);
        EventService.Instance.OnUnpauseGame.RemoveListener(UnPauseGame);
    }

    private void Start()
    {
        _matchesWin = _itemsToSpawn / pair;
        _score = 1;
        
        if(AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayAmbience(1);
        }

        if(_timerRoutine == null)
        {
            _timerRoutine = StartCoroutine(TimerRoutine());
        }
    }

    private void OnUncoverItem(TileItem item)
    {
        if(_checkingCoroutine == null)
        _checkingCoroutine = StartCoroutine(CheckRoutine(item));
    }

    private void CleanList()
    {
        _currentReveleadItems = 0;
        _namesRevealed.Clear();
    }

    private IEnumerator CheckRoutine(TileItem item)
    {
        _currentReveleadItems++;
        _namesRevealed.Add(item);

        for (int i = 0; i < _namesRevealed.Count; i++)
        {
            for (int j = i + 1; j < _namesRevealed.Count; j++)
            {
                if (_namesRevealed[j].Name == _namesRevealed[i].Name)
                {
                    EventService.Instance.OnItemsMatch.InvokeEvent();
                    foreach (TileItem itemTile in _namesRevealed)
                    {
                        itemTile.OnItemMatch();
                    }

                    if(AudioManager.Instance !=null)
                    {
                        AudioManager.Instance.PlaySfxClip(0);
                    }

                    _currentMatchs++;
                    CheckWin();
                    yield return new WaitForSeconds(_checkingTime);
                    CleanList();
                }

                else
                {
                    EventService.Instance.OnItemCover.InvokeEvent();

                    //Reset Items Uncovered and Clean List
                    yield return new WaitForSeconds(_checkingTime);
                    CleanList();
                }
            }
            _checkingCoroutine = null;
        }
    }

    private void CheckWin()
    {
        if (_currentMatchs == _matchesWin)
        {
            _isGameOver = true;
            _score *= Mathf.RoundToInt(((int)_timer * 2) / 100f) * 100;

            //Save LastScore
            PlayerPrefs.SetInt("LastScore", _score);
            PlayerPrefs.Save();
            EventService.Instance.OnEndGame.InvokeEvent(_score);
        }
    }

    private IEnumerator TimerRoutine()
    {
        while(_timer > 0 && !_isGameOver)
        {
            _timer -= Time.deltaTime;
            _textTimer.text = Mathf.CeilToInt(_timer).ToString();
            yield return null;
        }

        if(!_isGameOver)
        {
            _isGameOver = true;
            EventService.Instance.OnGameOver.InvokeEvent();
            _timerRoutine = null;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
    }
}
