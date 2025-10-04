using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TileItem : MonoBehaviour
{
    [Header("Item Settings")]
    [SerializeField] private Image _sprite;
    [SerializeField] private string _name;
    public string Name { get { return _name; } }
    [SerializeField] private Image _cover;
    [SerializeField] private float _uncoverTime = 1.5f;

    private bool _isUncovered;
    private bool _isMatched;
    private int _pairItems = 2;
    private Coroutine _coverRoutine;
    private TileGameManager _tileGameManager;

    private void OnEnable()
    {
        EventService.Instance.OnItemCover.AddListener(CoverRoutine);
    }

    private void OnDisable()
    {
        EventService.Instance.OnItemCover.RemoveListener(CoverRoutine);
    }

    private void Start()
    {
        _tileGameManager = GameObject.Find("GameManager").GetComponent<TileGameManager>();
        if(_tileGameManager == null)
        {
            Debug.LogError("TileManager is null on Item");
        }
    }


    public void SetItemInfo(Sprite sprite, string name)
    {
       _sprite.sprite = sprite;
       _name = name;
    }

    public void UncoverItem()
    {
        if(_tileGameManager.CurrentRevealedItems < _pairItems && !_isUncovered && !_tileGameManager.IsGameOver)
        {
            _cover.gameObject.SetActive(false);
            _isUncovered = true;
            EventService.Instance.OnUncoverItem.InvokeEvent(this);
        }
    }

    private void CoverRoutine()
    {
        if(_coverRoutine == null && !_isMatched)
        {
            _coverRoutine = StartCoroutine(CoverItem());
        }
    }

    private IEnumerator CoverItem()
    {
        yield return new WaitForSeconds(_uncoverTime);
        _cover.gameObject.SetActive(true);
        _isUncovered = false;
        _coverRoutine = null;
    }

    public void OnItemMatch()
    {
        _isMatched = true;
    }
}
