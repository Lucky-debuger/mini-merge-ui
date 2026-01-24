using UnityEngine;

public class Slot
{
    public Chip Chip { get; private set; }
    public bool IsEmpty => Chip == null;
    public int Index { get; private set; }

    public void Initialize(int index)
    {
        Index = index;
    }

    public void SetChip(Chip chip)
    {
        if (IsEmpty) Chip = chip;
    }

    public void ClearChip()
    {
        Chip = null;
    }
}
