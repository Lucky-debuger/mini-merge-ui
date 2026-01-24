using UnityEngine;

public class Slot
{
    public Chip Chip { get; private set; }
    public bool IsEmpty => Chip == null;
    public int Index { get; private set; }

    public Slot(int index)
    {
        Index = index;
    }

    public void SetChip(Chip chip)
    {
        if (!IsEmpty)
        {
            Debug.LogError($"Slot is not empty {Index}");
            return;
        }

        Chip = chip;
    }

    public void ClearChip()
    {
        Chip = null;
    }
}
