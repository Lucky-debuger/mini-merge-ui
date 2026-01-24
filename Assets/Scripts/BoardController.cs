using System.Collections.Generic;
using UnityEngine;

public class BoardController
{
    private ChipView _chipViewPrefab;
    private SlotView _slotViewPrefab;
    private Transform _contentTransform;

    private Dictionary<int, Slot> _slotModels = new Dictionary<int, Slot>();
    private Dictionary<int, SlotView> _slotViews = new Dictionary<int, SlotView>();
    private List<Slot> _freeSlotModels = new List<Slot>();

    private System.Random _random = new System.Random();
    
    public BoardController(ChipView chipModelPrefab, SlotView slotViewPrefab, Transform contentTransform)
    {
        _contentTransform = contentTransform;
        _chipViewPrefab = chipModelPrefab;
        _slotViewPrefab = slotViewPrefab;

        CreateSlotViews();
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
        Chip chipModel = new Chip(ChipType.Level1);
        slotModel.SetChip(chipModel);
        _freeSlotModels.Remove(slotModel);
        ChipView chipView = GameObject.Instantiate(_chipViewPrefab, _slotViews[slotModel.Index].transform);
        chipView.Init(chipModel);
    }

    public void TryMerge()
    {
        
    }
}
