using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class MathManager : MonoBehaviour
{
    [Header("Numbers UI")]
    [SerializeField] private TextMeshProUGUI _firstNumber;
    [SerializeField] private TextMeshProUGUI _secondNumber;

    [Header("Numbers Settings")]
    [SerializeField] private int _minNumb = 0;
    [Range(0,50)]
    [SerializeField] private int _maxNumb;

    private int _itemsToSpawn;
    private int _result;
    [SerializeField] List<int> _numbers = new List<int>();

    private void Start()
    {
        _itemsToSpawn = GetComponent<GridSpawner>().ItemsToSpawn;

        Debug.Log("ItemsToSpawn: " +  _itemsToSpawn);
        if(_itemsToSpawn != 0)
        {
            GenerateQuestion();
            AssignNumbers();
        }
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
        EventService.Instance.OnSetAnswer.InvokeEvent(_result);
    }

    private void AssignNumbers()
    {
        while(_numbers.Count <= _itemsToSpawn)
        {
            int randomNum = Random.Range(0, _result + 10);
            Debug.Log("Should Add number");
            if (!_numbers.Contains(randomNum))
            {
                _numbers.Add(randomNum);
            }
        }

        if (!_numbers.Contains(_result))
        {
            _numbers[Random.Range(0,_numbers.Count)] = _result;
        }

        EventService.Instance.OnSpawnItems.InvokeEvent(_numbers);
    }
}

