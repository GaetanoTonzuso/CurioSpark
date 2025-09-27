using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GridSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefabToSpawn;
    [Range(1,15)]
    [SerializeField] private int _itemsToSpawn;
    public int ItemsToSpawn { get { return _itemsToSpawn; }}
    [SerializeField] private GameObject _parent;

    private void OnEnable()
    {
        EventService.Instance.OnSpawnItems.AddListener(SpawnItemsInGrid);
        
    }

    private void OnDisable()
    {
        EventService.Instance.OnSpawnItems.RemoveListener(SpawnItemsInGrid);
    }

    private void SpawnItemsInGrid(List<int> list)
    {
        for(int i = 0; i <= _itemsToSpawn; i++)
        {
            GameObject barrell = Instantiate(_prefabToSpawn,transform.position,Quaternion.identity);
            barrell.transform.SetParent(_parent.transform,true);
            TextMeshProUGUI _textBarrel = barrell.GetComponentInChildren<TextMeshProUGUI>();
            if ( _textBarrel != null )
            {
                _textBarrel.text = list[i].ToString();
            }
        }
    }
}
