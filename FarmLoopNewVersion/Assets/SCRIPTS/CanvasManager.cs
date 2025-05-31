using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class CanvasManager : Singleton<CanvasManager>
{
    [SerializeField] private TextMeshProUGUI shovelCount;
    [SerializeField] private TextMeshProUGUI dynamiteCount;
    [SerializeField] private TextMeshProUGUI goldCount;

    [SerializeField] private GameObject ShopMenu;

    [SerializeField] private ItemData carrot;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
    {
        updateShovelCount (InventoryManager.Instance.shovelInventoryAmount);
        updateDynamiteCount(InventoryManager.Instance.dynamiteInventoryAmount);
        updateGoldcount(InventoryManager.Instance.goldInventoryAmount);
    }

    public void AddCarrot()
    {
        TownHallStorage.Instance.AddItem(carrot, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateGoldcount(int newGoldCount)
    {
        goldCount.text = newGoldCount.ToString();
        InventoryManager.Instance.goldInventoryAmount = newGoldCount;
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

    public void CloseShopMenu()
    {
        if (ShopMenu.gameObject.activeSelf)
        {
            Debug.Log("CalledClose");
            GameManager.Instance.shopOpen = false;
            ShopMenu.SetActive(false);
        }

        
    }
    public void OpenShopMenu()
    {
        if(GameManager.Instance.buildOpen == false)
        {
            if (ShopMenu.gameObject.activeSelf == false)
            {
                Debug.Log("CalledOpen");
                GameManager.Instance.shopOpen = true;
                ShopMenu.SetActive(true);

                foreach (GameObject obstacles in GridManager.Instance._gridGenerator.allObstacles)
                {
                    obstacles.GetComponent<RemoveObstacles>().DesactivateDestroyUI();
                }

            }
        }
    }
}
