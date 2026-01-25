using System;
using System.Linq;

public enum ChipType
{
    Level0 = 0,
    Level1 = 1,
    Level2 = 2,
}

public class Chip
{
    public ChipType Type { get; private set; }

    public Chip(ChipType chipType)
    {
        Type = chipType;
    }

    public void Upgrade()
    {
        var max = Enum.GetValues(typeof(ChipType)).Cast<ChipType>().Max(); // [ ] Как работает данная строка?
        ChipType nextType = (ChipType)Math.Min((int)Type + 1, (int)max);
        Type = nextType; // [ ] Поменял логику
    }
}