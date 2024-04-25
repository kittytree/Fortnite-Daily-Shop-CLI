using System;
namespace FortniteShopSort;

public sealed class FortniteItem : IComparable<FortniteItem>
{
    private readonly ItemType Type;
    private readonly int Cost;
    private readonly string? Theme;
    private readonly string Name;

    public FortniteItem(ItemType Type, int Cost, string? Theme, string Name)
    {
        this.Type = Type;
        this.Cost = Cost;
        this.Theme = Theme;
        this.Name = Name;
    }

    public int CompareTo(FortniteItem? other)
    {
        if (other == null) return -1;
        if (Type != other.Type) return Type.CompareTo(other.Type);
        if (Cost != other.Cost) return Cost.CompareTo(other.Cost);
        if (Theme == null) return -1;
        if (other.Theme == null) return 1;
        if (Theme != other.Theme) return Theme.CompareTo(other.Theme);
        return 0;
        
    }

    public override string ToString()
    {
        return $"{Name} the {Type} within the {Theme} at {Cost} VBucks.";
    }
}
