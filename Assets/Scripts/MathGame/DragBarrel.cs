using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragBarrel : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private bool _canDrag = true;

    private Vector3 _startingPos;

    private void Start()
    {
        _startingPos = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_canDrag)
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.raycastTarget = true;
    }

}
