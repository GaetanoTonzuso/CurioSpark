using UnityEngine;
using UnityEngine.UI;

public class TileItem : MonoBehaviour
{
    [Header("Item Settings")]
    [SerializeField] private Image _sprite;
    [SerializeField] private string _name;
    [SerializeField] private Image _cover;


    public void SetItemInfo(Sprite sprite, string name)
    {
       _sprite.sprite = sprite;
       this._name = name;
    }
}
