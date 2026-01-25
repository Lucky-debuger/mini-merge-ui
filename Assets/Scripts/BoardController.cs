using System.Collections.Generic;
using UnityEngine;

public class BoardController
{
    private ChipView _chipViewPrefab;
    private SlotView _slotViewPrefab;
    private Transform _contentTransform;
    private Transform _canvasTransform;
    private CreateSlotButton _createSlotButton;

    private Dictionary<int, Slot> _slotModels = new Dictionary<int, Slot>();
    private Dictionary<int, SlotView> _slotViews = new Dictionary<int, SlotView>();
    private List<Slot> _freeSlotModels = new List<Slot>();

    private System.Random _random = new System.Random();

    
    public BoardController
    (
        ChipView chipViewPrefab, 
        SlotView slotViewPrefab, 
        Transform contentTransform, 
        Transform canvasTransform, 
        CreateSlotButton createSlotButton
    )

    {
        _contentTransform = contentTransform;
        _canvasTransform = canvasTransform;
        _chipViewPrefab = chipViewPrefab;
        _slotViewPrefab = slotViewPrefab;
        _createSlotButton = createSlotButton;

        CreateSlotViews();

        _createSlotButton.OnButtonClick += CreateChipInRandomSlot;
    }

    private void CreateSlotViews()
    {
        for (int i = 0; i < 9; i++)
        {
            SlotView slotInstance = GameObject.Instantiate(_slotViewPrefab, _contentTransform);
            Slot slotModel = new Slot(i);
            slotInstance.Init(slotModel);
            _slotViews.Add(i, slotInstance);
            _slotModels.Add(i, slotModel);
            _freeSlotModels.Add(slotModel);
        }
    }

    public void CreateChipInRandomSlot()
    {
        if (_freeSlotModels.Count == 0)
        {
            Debug.LogError("No free slots");
            MessageController.Instance.ShowMessage("No free slots");

            return;
        }

        int freeIndex = _random.Next(0, _freeSlotModels.Count);

        Slot slotModel = _freeSlotModels[freeIndex];
        Chip chipModel = new Chip(ChipType.Level0);

        slotModel.SetChip(chipModel);
        _freeSlotModels.Remove(slotModel);

        ChipView chipView = GameObject.Instantiate
        (
            _chipViewPrefab, 
            _slotViews[slotModel.Index].transform
        );

        chipView.SlotView = _slotViews[slotModel.Index];
        chipView.Init(chipModel, _canvasTransform, this);
    }

    public bool TryMerge(Slot fromSlot, Slot toSlot)
    {
        if (fromSlot == null || toSlot == null) return false;

        else if (fromSlot.Chip.Type != toSlot.Chip.Type)
        {
            Debug.LogError("Different types of slots");
            MessageController.Instance.ShowMessage("Different types of slots");

            return false;
        }

        else if (fromSlot.Chip.IsMaxLevel || toSlot.Chip.IsMaxLevel) // [ ] Осознаю, что создаю аллокация, можно сделать и другим способом
        {
            Debug.LogError("Max level");
            MessageController.Instance.ShowMessage("Max level");

            return false;
        }

        else if (fromSlot.Chip.Type == toSlot.Chip.Type)
        {
            toSlot.Chip.Upgrade();
            fromSlot.ClearChip();
            _freeSlotModels.Add(fromSlot);

            return true;
        }

        return false;
    }

    public void ChangeSlot(Slot fromSlot, Slot toSlot)
    {
        toSlot.SetChip(fromSlot.Chip);
        fromSlot.ClearChip();

        _freeSlotModels.Remove(toSlot);
        _freeSlotModels.Add(fromSlot);

    }
}