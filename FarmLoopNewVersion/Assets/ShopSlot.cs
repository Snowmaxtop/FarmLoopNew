using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Data.SqlTypes;

public class ShopSlot : MonoBehaviour
{
    public Image itemIcon;
    [SerializeField] TextMeshProUGUI itemValueSlot;
    [SerializeField] TextMeshProUGUI amountInInventory;
    public ItemData itemDataReceived;


    public void UpdateSlotInformation(ItemData itemDataSent)
    {
        itemDataReceived = itemDataSent;
        itemIcon.sprite = itemDataReceived.icon;
        itemValueSlot.text = itemDataReceived.baseValue.ToString();
        int currentAmount = TownHallStorage.Instance.GetItemAmount(itemDataReceived);
        amountInInventory.text = "Inventory: " + currentAmount.ToString();
    }

    public void SellOneItem()
    {

        if (itemDataReceived == null)
        {
            Debug.LogWarning("SellOneItem called with null item.");
            return;
        }

        if (TownHallStorage.Instance.storedItems.TryGetValue(itemDataReceived, out int quantity) && quantity > 0)
        {
            // Remove one item from storage
            TownHallStorage.Instance.RemoveItem(itemDataReceived, 1);
            //Update Amount on Slot
            UpdateSlotInformation(itemDataReceived);
            // Add item's value to player's gold
            AddGoldToPlayer(itemDataReceived.baseValue);

            Debug.Log($"Sold 1x {itemDataReceived.itemName} for {itemDataReceived.baseValue} gold.");
            UpdateSlotInformation(itemDataReceived);
        }
        else
        {
            Debug.LogWarning($"No {itemDataReceived.itemName} in storage to sell.");
        }
    }
    private void AddGoldToPlayer(int amount)
    {
        int newAmount = InventoryManager.Instance.goldInventoryAmount += amount;
        CanvasManager.Instance.updateGoldcount(newAmount);
        // Or update your actual player gold logic here instead
    }

}
