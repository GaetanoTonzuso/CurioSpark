using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class TileGameManager : MonoBehaviour
{
    //Variables

    private int _currentReveleadItems;
    public int CurrentRevealedItems { get { return _currentReveleadItems; } }

    private bool _isChecking;
    private Coroutine _checkingCoroutine;

    [SerializeField] private float _checkingTime = 3f;
    [SerializeField] private List<TileItem> _namesRevealed = new List<TileItem>();

    //Variables

    private void OnEnable()
    {
        EventService.Instance.OnUncoverItem.AddListener(OnUncoverItem);
    }

    private void OnDisable()
    {
        EventService.Instance.OnUncoverItem.RemoveListener(OnUncoverItem);
    }

    private void Start()
    {
        AudioManager.Instance.PlayAmbience(1);
    }

    private void OnUncoverItem(TileItem item)
    {
        //if (_isChecking) return;

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
                    Debug.Log("Found same items");
                    EventService.Instance.OnItemsMatch.InvokeEvent();
                    foreach (TileItem itemTile in _namesRevealed)
                    {
                        itemTile.OnItemMatch();
                    }
                    yield return new WaitForSeconds(_checkingTime);
                    CleanList();
                }

                else
                {
                    Debug.Log("Items not matching");
                    EventService.Instance.OnItemCover.InvokeEvent();

                    //Reset Items Uncovered and Clean List
                    yield return new WaitForSeconds(_checkingTime);
                    CleanList();
                }
            }
            _checkingCoroutine = null;
        }
    }
}
