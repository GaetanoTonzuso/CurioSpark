using UnityEngine;
using UnityEngine.EventSystems;

public class DropBarrellArea : MonoBehaviour, IDropHandler
{
    private int _requiredNumber;
    private MathManager _gameManager;

    private void OnEnable()
    {
        EventService.Instance.OnSetAnswer.AddListener(SetResult);
    }

    private void OnDisable()
    {
        EventService.Instance.OnSetAnswer.RemoveListener(SetResult);
    }

    private void Start()
    {
        _gameManager  = GameObject.Find("GameManager").GetComponent<MathManager>();
        if(_gameManager == null)
        {
            Debug.LogError("Math Manager is NULL on DropBarrell");
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        DropItemOnPosition(eventData);
    }

    private void DropItemOnPosition(PointerEventData eventData)
    {
        Barrell barrel = eventData.pointerDrag.GetComponent<Barrell>();
        if (barrel != null)
        {
            eventData.pointerDrag.transform.position = transform.position;

            //Disable CanDrag bool - item cannot be dragged once in the position
            if (eventData.pointerDrag.TryGetComponent<DragBarrel>(out DragBarrel draggable) && draggable.canDrag)
            {
                draggable.SetDragStatus();
            }

            //Check if barrels answer is the required one
            if (barrel.Number == _requiredNumber)
            {
                //Send communication to UIMathGame
                EventService.Instance.OnUpdateFeedback.InvokeEvent("Correct!");

                //Update Score UI and Score on Game Manager
                EventService.Instance.OnUpdateScore.InvokeEvent();
                int currentScore = _gameManager.Score;
                EventService.Instance.OnUpdateScoreUI.InvokeEvent(currentScore);
            }
            else
            {
                //Send communication to UIMathGame
                EventService.Instance.OnUpdateFeedback.InvokeEvent("Not Correct, answer is: " + _requiredNumber);
                AudioManager.Instance.PlaySfxClip(1);
            }

            //Communicate with GameManager and UI for new questions
            if(!_gameManager.IsGameOver())
            EventService.Instance.OnGenerateNewQuestion.InvokeEvent();

        }
    }

    private void SetResult(int requiredNumber)
    {
        _requiredNumber = requiredNumber;
    }
}
