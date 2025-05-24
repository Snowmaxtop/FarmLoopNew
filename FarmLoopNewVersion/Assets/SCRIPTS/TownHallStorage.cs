using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemAmountEntry
{
    public ItemData item;
    public int amount;
}

public class TownHallStorage : MonoBehaviour
{
    // Serialized view for inspector
    [SerializeField] private List<ItemAmountEntry> itemEntries = new List<ItemAmountEntry>();

    // Runtime dictionary
    private Dictionary<ItemData, int> storedItems = new Dictionary<ItemData, int>();

    void Awake()
    {
        BuildDictionaryFromList();

        foreach (var item in ShopManager.Instance.tier0List)
            AddItem(item, 0);
        foreach (var item in ShopManager.Instance.tier1List)
            AddItem(item, 0);
        foreach (var item in ShopManager.Instance.tier2List)
            AddItem(item, 0);

        UpdateSerializedList(); // Reflect any new items in the inspector
    }

    // Convert list to dictionary
    private void BuildDictionaryFromList()
    {
        storedItems.Clear();
        foreach (var entry in itemEntries)
        {
            if (entry.item != null)
                storedItems[entry.item] = entry.amount;
        }
    }

    // Convert dictionary back to list for inspector sync
    private void UpdateSerializedList()
    {
        itemEntries.Clear();
        foreach (var kvp in storedItems)
        {
            itemEntries.Add(new ItemAmountEntry { item = kvp.Key, amount = kvp.Value });
        }
    }

    // Add items
    public void AddItem(ItemData item, int amount)
    {
        if (storedItems.ContainsKey(item))
            storedItems[item] += amount;
        else
            storedItems[item] = amount;

        UpdateSerializedList();
    }

    // Remove items
    public bool RemoveItem(ItemData item, int amount)
    {
        if (storedItems.ContainsKey(item) && storedItems[item] >= amount)
        {
            storedItems[item] -= amount;
            if (storedItems[item] == 0)
                storedItems.Remove(item);

            UpdateSerializedList();
            return true;
        }

        return false;
    }

    // Get current amount
    public int GetItemAmount(ItemData item)
    {
        return storedItems.TryGetValue(item, out int amount) ? amount : 0;
    }

    // Optional: for debugging
    public void PrintStorage()
    {
        foreach (var entry in storedItems)
        {
            Debug.Log($"{entry.Key.itemName}: {entry.Value}");
        }
    }
}
