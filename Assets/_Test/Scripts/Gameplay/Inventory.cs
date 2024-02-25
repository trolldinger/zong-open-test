using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<InventoryItem> Items;

    private void Awake() 
    {
        if(Instance != null)
            Destroy(Instance);
        Instance = this;
    }

    public bool HasItem(IItem item)
    {
        return Items.Find(x=>x.ItemName == item.ItemName) != null;
    }

    public InventoryItem[] GetAllWeapons()
    {
        return Items.FindAll(x => x.Type == ItemType.Weapon).ToArray();
    }

    public InventoryItem[] GetAllInstruments()
    {
        return Items.FindAll(x => x.Type == ItemType.Instrument).ToArray();
    }

    public void AddItem(IItem item)
    {
        Items.Add(new InventoryItem(){ItemName = item.ItemName,Type = item.ItemType});
    }

    public void RemoveItem(IItem item)
    {
        Items.Remove(Items.Find(x=>x.ItemName == item.ItemName));
    }
}

[System.Serializable]
public class InventoryItem
{
    public string ItemName;
    public ItemType Type;
}