using UnityEngine;
using UnityEngine.UI;

public class BuildMenu : MonoBehaviour
{
    [Header("GameObject Reference")]
    [SerializeField] private GameObject buildMenu;
    [SerializeField] private GameObject generatorsScrollView;
    [SerializeField] private GameObject convertorsScrollView;
    [SerializeField] private GameObject decorationsScrollView;

    [Space(10f)]
    [Header("Tabs")]
    [SerializeField] private Image generatorsTab;
    [SerializeField] private Image convertorsTab;
    [SerializeField] private Image decorationsTab;
    
    [Space(10f)]
    [Header("Colors")]
    [SerializeField] private Color baseColor;
    [SerializeField] private Color baseColorDark;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseColor = generatorsTab.color;
        generatorsScrollView.SetActive(true);
        convertorsScrollView.SetActive(false);
        decorationsScrollView.SetActive(false);
    }

    public void OpenBuildMenu()
    {
        if (GameManager.Instance.shopOpen == false)
        {
            buildMenu.SetActive(true);
            GameManager.Instance.buildOpen = true;
            SwitchToGenerators();
        }
    }

    public void CloseBuildMenu()
    {
        buildMenu.SetActive(false);
        GameManager.Instance.buildOpen = false;
    }

    public void SwitchToGenerators()
    {
        generatorsTab.color = baseColorDark;
        convertorsTab.color = baseColor;
        decorationsTab.color = baseColor;

        generatorsScrollView.SetActive(true);
        convertorsScrollView.SetActive(false);
        decorationsScrollView.SetActive(false);
    }

    public void SwitchToConvertors()
    {
        generatorsTab.color = baseColor;
        convertorsTab.color = baseColorDark;
        decorationsTab.color = baseColor;

        generatorsScrollView.SetActive(false);
        convertorsScrollView.SetActive(true);
        decorationsScrollView.SetActive(false);
    }

    public void SwitchToDecorations()
    {
        generatorsTab.color = baseColor;
        convertorsTab.color = baseColor;
        decorationsTab.color = baseColorDark;
        
        generatorsScrollView.SetActive(false);
        convertorsScrollView.SetActive(false);
        decorationsScrollView.SetActive(true);
    }

}

