using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileGameManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _icons;
    [SerializeField] private string[] _names;
    private int randomIndex;

    [Header("Grid Settings")]
    [SerializeField] private GameObject _grid;
    [SerializeField] private GameObject _item;
    [SerializeField] private int _itemsToSpawn;

    private void Start()
    {
        GenerateGridItems();
    }

    private void GenerateGridItems()
    {
        List<Sprite> spritePool;
        List<string> namePool;
        GeneratePools(_itemsToSpawn, out spritePool, out namePool);

        for (int i = 0; i < _itemsToSpawn; ++i)
        {
            GameObject itemClone = Instantiate(_item, _grid.transform.position,Quaternion.identity);
            itemClone.transform.SetParent(_grid.transform, true);
            TileItem tile = itemClone.GetComponent<TileItem>();
            if (tile != null)
            {
                tile.SetItemInfo(spritePool[i], namePool[i]);
            }
        }
    }

    private void GeneratePools(int itemsToSpawn, out List<Sprite> spritePool, out List<string> namePool)
    {
        spritePool = new List<Sprite>();
        namePool = new List<string>();

        int pairs = itemsToSpawn / 2;

        for (int i = 0; i < pairs; i++)
        {
            int index = Random.Range(0, _icons.Length);

            spritePool.Add(_icons[index]);
            spritePool.Add(_icons[index]);

            namePool.Add(_names[index]);
            namePool.Add(_names[index]);
        }

        // shuffle
        for (int i = 0; i < spritePool.Count; i++)
        {
            int randomIndex = Random.Range(i, spritePool.Count);

            // change sprite
            Sprite tempSprite = spritePool[i];
            spritePool[i] = spritePool[randomIndex];
            spritePool[randomIndex] = tempSprite;

            // change name on same order
            string tempName = namePool[i];
            namePool[i] = namePool[randomIndex];
            namePool[randomIndex] = tempName;
        }
    }

}
