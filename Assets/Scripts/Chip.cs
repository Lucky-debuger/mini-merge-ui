public enum ChipType // [ ] Разобраться, что это такое? 
{
    Level1 = 0,
    Level2 = 1,
    Level3 = 2,
    Level4 = 3
}

public class Chip
{
    public ChipType Type { get; private set; }

    public Chip(ChipType type)
    {
        Type = type;
    }

    public Chip Upgrade()
    {
        var next = (int)Type + 1;
        if (next > (int)ChipType.Level4) next = (int)ChipType.Level4;
        return new Chip((ChipType)next); // [ ] Что за странная запись в скобках?
    }
}
