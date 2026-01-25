using System.Collections.Generic;
using UnityEngine;

public class BoardController
{
    private ChipView _chipViewPrefab;
    private SlotView _slotViewPrefab;
    private Transform _contentTransform;
    private Transform _canvasTransform;

    private Dictionary<int, Slot> _slotModels = new Dictionary<int, Slot>();
    private Dictionary<int, SlotView> _slotViews = new Dictionary<int, SlotView>();
    private List<Slot> _freeSlotModels = new List<Slot>();

    private System.Random _random = new System.Random();
    
    public BoardController(ChipView chipViewPrefab, SlotView slotViewPrefab, Transform contentTransform, Transform canvasTransform)
    {
        _contentTransform = contentTransform;
        _canvasTransform = canvasTransform;
        _chipViewPrefab = chipViewPrefab;
        _slotViewPrefab = slotViewPrefab;

        CreateSlotViews();
        CreateChipInRandomSlot(); // [ ] Для теста не забудь удалить
        CreateChipInRandomSlot();
        CreateChipInRandomSlot();
        CreateChipInRandomSlot();
        CreateChipInRandomSlot();
        CreateChipInRandomSlot();
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
        chipView.Init(chipModel, _canvasTransform, this); // [ ] Стоит ли так делать?
    }

    public bool TryMerge(Slot fromSlot, Slot toSlot)
    {
        if (fromSlot == null || toSlot == null) return false;

        if (fromSlot.Chip.Type == toSlot.Chip.Type)
        {
            toSlot.Chip.Upgrade();
            fromSlot.ClearChip();
            return true;
        }
        return false;
    }

    public void ChangeSlot(Slot fromSlot, Slot toSlot)
    {
        toSlot.SetChip(fromSlot.Chip);
        fromSlot.ClearChip();
    }
}
