using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private TextMeshProUGUI timerText;
    public float totalTime = 20f; // 20 minutes = 20 * 60 seconds
    private float currentTime;
    public bool shopOpen = false;
    public bool buildOpen = false;

    void Start()
    {
        currentTime = totalTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (shopOpen)
        {
            ShopManager.Instance.UpdateTimerDisplay(currentTime, totalTime);
        }

        if (currentTime <= 0f)
        {
            ResetShopTimer();
        }
    }
    private void ResetShopTimer()
    {
        if (currentTime <= 0f)
        {
            //ResetShop;
            currentTime = totalTime; // Reset the timer
            ShopManager.Instance.ResetShop();
        }
    }

}
