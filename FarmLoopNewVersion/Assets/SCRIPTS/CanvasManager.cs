using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class CanvasManager : Singleton<CanvasManager>
{
    [SerializeField] private TextMeshProUGUI shovelCount;
    [SerializeField] private TextMeshProUGUI dynamiteCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updateShovelCount (InventoryManager.Instance.shovelInventoryAmount);
        updateDynamiteCount(InventoryManager.Instance.dynamiteInventoryAmount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateShovelCount(int newShovelCount)
    {
        shovelCount.text = newShovelCount.ToString();
        InventoryManager.Instance.shovelInventoryAmount = newShovelCount;
    }
    public void updateDynamiteCount(int newDynamiteCount)
    {
        dynamiteCount.text = newDynamiteCount.ToString();
        InventoryManager.Instance.dynamiteInventoryAmount = newDynamiteCount;
    }
}
