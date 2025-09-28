using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragBarrel : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private bool _canDrag = true;
    public bool canDrag { get { return _canDrag; } }

    [SerializeField] private Vector3 _startingPos;


    public void OnBeginDrag(PointerEventData eventData)
    {
        if(_canDrag)
        {
            _startingPos = transform.localPosition;
            _image.raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_canDrag)
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.raycastTarget = true;
        if(eventData.pointerEnter.TryGetComponent<DropBarrellArea>(out DropBarrellArea dropArea))
        {
            return;
        }

        _image.rectTransform.localPosition = _startingPos;
    }

    public void SetDragStatus()
    {
        _canDrag = false;
    }
}
