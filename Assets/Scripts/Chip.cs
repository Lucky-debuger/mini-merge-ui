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
    
    public bool IsMaxLevel
    {
        get { return Enum.GetValues(typeof(ChipType)).Cast<ChipType>().Max() == Type; }
    }

    public Chip(ChipType chipType)
    {
        Type = chipType;
    }

    public void Upgrade()
    {
        var max = Enum.GetValues(typeof(ChipType)).Cast<ChipType>().Max();
        ChipType nextType = (ChipType)Math.Min((int)Type + 1, (int)max);
        Type = nextType;
    }
}