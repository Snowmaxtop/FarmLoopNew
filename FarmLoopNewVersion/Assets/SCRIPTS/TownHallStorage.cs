using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class TownHallStorage : MonoBehaviour
{
    private Dictionary<ItemData, int> storedItems = new Dictionary<ItemData, int>();

    // Add items
    public void AddItem(ItemData item, int amount)
    {
        if (storedItems.ContainsKey(item))
        {
            storedItems[item] += amount;
        }
        else
        {
            storedItems[item] = amount;
        }
    }

    // Remove items
    public bool RemoveItem(ItemData item, int amount)
    {
        if (storedItems.ContainsKey(item) && storedItems[item] >= amount)
        {
            storedItems[item] -= amount;
            if (storedItems[item] == 0)
                storedItems.Remove(item);

            return true;
        }

        return false; // Not enough items
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
