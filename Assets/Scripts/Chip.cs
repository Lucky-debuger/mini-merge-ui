using System;
using System.Linq;

public enum ChipType // [ ] Почему enum создается, как отдельный объект?
{
    Level0 = 0,
    Level1 = 1,
    Level2 = 2,
    Level3 = 3,
}

public class Chip
{
    public ChipType Type { get; private set; }

    public Chip(ChipType chipType)
    {
        Type = chipType;
    }

    public Chip Upgrade()
    {
        var max = Enum.GetValues(typeof(ChipType)).Cast<ChipType>().Max(); // [ ] Как работает данная строка?
        ChipType nextType = (ChipType)Math.Min((int)Type + 1, (int)max);

        return new Chip(nextType);
    }
}