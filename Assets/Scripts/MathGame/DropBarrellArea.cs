using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropBarrellArea : MonoBehaviour, IDropHandler
{
    private void OnEnable()
    {
        EventService.Instance.OnSetAnswer.AddListener(SetResult);
        //MathManager.SetAnswer += SetResult;
    }

    private void OnDisable()
    {
        EventService.Instance.OnSetAnswer.RemoveListener(SetResult);
    }

    private int _requiredNumber;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop ");
        Barrell barrel = eventData.pointerDrag.GetComponent<Barrell>();
        if(barrel != null)
        {
            eventData.pointerDrag.transform.position = transform.position;
            if(barrel.Number == _requiredNumber)
            {
                Debug.Log("Number Correct");
            }
            else
            {
                Debug.Log("Number incorrect!");
            }

        }
        this.gameObject.SetActive(false);
    }

    private void SetResult(int requiredNumber)
    {
        _requiredNumber = requiredNumber;
    }
}
