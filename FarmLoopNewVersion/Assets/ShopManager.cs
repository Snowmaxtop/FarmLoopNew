using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
{
    //FULL LIST OF ITEMS.
    public List<ItemData> tier0List;
    public List<ItemData> tier1List;
    public List<ItemData> tier2List;

    //SELECTED ITEMS FOR 20MINS.
    [SerializeField] private List<ItemData> tier0ItemSelected = new List<ItemData>();
    [SerializeField] private List<ItemData> tier1ItemSelected = new List<ItemData>();
    [SerializeField] private List<ItemData> tier2ItemSelected = new List<ItemData>();

    //PREVIOUS SELECTIONS
    private HashSet<ItemData> previousTier0Items = new HashSet<ItemData>();
    private HashSet<ItemData> previousTier1Items = new HashSet<ItemData>();
    private HashSet<ItemData> previousTier2Items = new HashSet<ItemData>();

    //UI ELEMENTS.
    [SerializeField] private List<GameObject> tier0Slots = new List<GameObject>();
    [SerializeField] private List<GameObject> tier1Slots = new List<GameObject>();
    [SerializeField] private List<GameObject> tier2Slots = new List<GameObject>();
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Image circleTime;

    public void Start()
    {
        SelectNewItems();
        RefreshSlots();
    }

    public void UpdateTimerDisplay(float time, float totalTime)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        timerText.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);

        float fillAmount = time / totalTime;
        circleTime.fillAmount = fillAmount;
    }

    public void ResetShop()
    {
        SelectNewItems();
        RefreshSlots();
    }

    private void SelectNewItems()
    {
        tier0ItemSelected = GetUniqueRandomItems(tier0List, previousTier0Items);
        tier1ItemSelected = GetUniqueRandomItems(tier1List, previousTier1Items);
        tier2ItemSelected = GetUniqueRandomItems(tier2List, previousTier2Items);

        // Update previous selections for influence next selection
        previousTier0Items = new HashSet<ItemData>(tier0ItemSelected);
        previousTier1Items = new HashSet<ItemData>(tier1ItemSelected);
        previousTier2Items = new HashSet<ItemData>(tier2ItemSelected);
    }

    private List<ItemData> GetUniqueRandomItems(List<ItemData> sourceList, HashSet<ItemData> previousItems)
    {
        List<ItemData> selectedItems = new List<ItemData>();
        List<ItemData> availableItems = new List<ItemData>(sourceList);

        System.Random rand = new System.Random();
        int safetyCounter = 0;

        while (selectedItems.Count < 3 && availableItems.Count > 0 && safetyCounter < 100)
        {
            safetyCounter++;
            int index = rand.Next(availableItems.Count);
            ItemData candidate = availableItems[index];

            if (selectedItems.Contains(candidate))
                continue;

            if (previousItems.Contains(candidate))
            {
                // Allow second chance with slightly higher duplication probability
                int secondRoll = rand.Next(2); // 0 or 1
                if (secondRoll == 0)
                    continue;
            }

            selectedItems.Add(candidate);
            availableItems.Remove(candidate);
        }

        return selectedItems;
    }

    private void RefreshSlots()
    {
        UpdateTierSlots(tier0Slots, tier0ItemSelected);
        UpdateTierSlots(tier1Slots, tier1ItemSelected);
        UpdateTierSlots(tier2Slots, tier2ItemSelected);
    }

    private void UpdateTierSlots(List<GameObject> slots, List<ItemData> items)
    {
        for (int i = 0; i < slots.Count && i < items.Count; i++)
        {
            slots[i].GetComponent<ShopSlot>().UpdateSlotInformation(items[i]);
        }
    }
}