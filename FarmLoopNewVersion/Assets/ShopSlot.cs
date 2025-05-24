using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    public Image itemIcon;
    public int itemCost;
    [SerializeField] TextMeshProUGUI itemValueSlot;
    public ItemData itemDataReceived;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSlotInformation(ItemData itemDataSent)
    {
        itemDataReceived = itemDataSent;
        itemIcon.sprite = itemDataReceived.icon;
        itemValueSlot.text = itemDataReceived.baseValue.ToString();
    }
}
