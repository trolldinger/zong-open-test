public interface IItem
{
    string ItemName{get;}
    ItemType ItemType{get;}
}


public enum ItemType
{
    Weapon,
    Instrument
}