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
        return Items.Contains(Items.Find(x=>x.ItemName == item.ItemName));
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