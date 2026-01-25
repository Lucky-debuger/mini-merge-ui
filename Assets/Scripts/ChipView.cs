using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChipView : MonoBehaviour
{
    public Chip ChipModel { get; private set; }
    public SlotView SlotView { get; set; }
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] levelSprites;

    private bool _isDragging;
    private Vector2 _startLocalPosition;
    private SlotView _startSlotView;

    private Transform _canvasTransform;
    private BoardController _boardController;


    public void Init(Chip chipModel, Transform contentTransform, BoardController boardController)
    {
        ChipModel = chipModel;
        _canvasTransform = contentTransform;
        _boardController = boardController;
        Refresh();
    }

    public void Refresh()
    {
        image.sprite = levelSprites[(int)ChipModel.Type];
    }

    private void Update()
    {
        if (_isDragging)
        {
            this.transform.position = Input.mousePosition;
        }
    }

    public void BeginDrag()
    {
        _isDragging = true;
        _startLocalPosition = transform.localPosition;
        _startSlotView = SlotView;
        this.transform.SetParent(_canvasTransform, false);
        this.transform.SetAsLastSibling();
    }

    public void EndDrag()
    {
        _isDragging = false;

        SlotView slotViewUnderPointer = FindSlotViewUnderPointer();

        if (slotViewUnderPointer == null || slotViewUnderPointer == SlotView)
        {
            ReturnBack();
            return;
        }

        else if (slotViewUnderPointer.SlotModel.IsEmpty)
        {
            _boardController.ChangeSlot(SlotView.SlotModel, slotViewUnderPointer.SlotModel);
            SlotView = slotViewUnderPointer;
            this.transform.SetParent(slotViewUnderPointer.transform, false);
            this.transform.position = slotViewUnderPointer.transform.position;
            return;
        }

        bool merged = _boardController.TryMerge(SlotView.SlotModel, slotViewUnderPointer.SlotModel); // [ ] Хорошая ли идея, что он сам обращается?

        if (merged)
        {
            ChipView targetChipView = slotViewUnderPointer.transform.GetChild(0).GetComponent<ChipView>();
            targetChipView.Refresh();
            Debug.Log(targetChipView.ChipModel.Type);
            Destroy(this.gameObject);
        }

        else
        {
            ReturnBack();
        }
    }

    private void ReturnBack()
    {
        this.transform.localPosition = _startLocalPosition;
        this.transform.SetParent(_startSlotView.transform, false); // [ ] Если изменить на SetParent, то появляется баг, что позиция chipview сдвигается
        this.transform.SetAsFirstSibling();
    }

    private SlotView FindSlotViewUnderPointer()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            SlotView slotView = result.gameObject.GetComponent<SlotView>();
            if (slotView != null)
            {
                return slotView;
            }
        }

        return null;
    }
}