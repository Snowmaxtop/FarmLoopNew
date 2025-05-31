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
    [Header("Viewports")]
    [SerializeField] private GameObject generatorsContent;
    [SerializeField] private GameObject convertorsContent;
    [SerializeField] private GameObject decorationsContent;

    [Space(10f)]
    [Header("Colors")]
    [SerializeField] private Color baseColor;
    [SerializeField] private Color baseColorDark;

    [SerializeField] private GameObject buildMenuButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseColor = generatorsTab.color;
        generatorsScrollView.SetActive(true);
        convertorsScrollView.SetActive(false);
        decorationsScrollView.SetActive(false);
        InstantiateGeneratorButtons();
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

    public void InstantiateGeneratorButtons()
    {
        foreach (var puzzlePieces in GridManager.Instance._puzzlePiecesManager.generatorPuzzlePieces)
        {
            Transform parentTransform = generatorsContent.transform;
            GameObject instantiatedButton = Instantiate(buildMenuButton, parentTransform.position, Quaternion.identity);
            instantiatedButton.transform.SetParent(generatorsContent.transform);
            instantiatedButton.transform.localPosition = Vector3.zero;
            instantiatedButton.GetComponent<BuildMenuButtonPP>().puzzlePieceHolder = puzzlePieces.GetComponent<PuzzlePiece>();
            instantiatedButton.GetComponent<BuildMenuButtonPP>().UpdateButtonVisuals();
            Debug.Log("Called");
        }
    }



    public void SortPiecesByLevel()
    {
        if (generatorsContent == null) return;

        Transform[] children = new Transform[generatorsContent.transform.childCount];
        for (int i = 0; i < generatorsContent.transform.childCount; i++)
            children[i] = generatorsContent.transform.GetChild(i);

        System.Array.Sort(children, (a, b) =>
        {
            var buttonA = a.GetComponent<BuildMenuButtonPP>();
            var buttonB = b.GetComponent<BuildMenuButtonPP>();
            if (buttonA == null || buttonB == null) return 0;

            var levelA = buttonA.puzzlePieceHolder?.PuzzlePieceSO?.puzzlePieceLevel ?? 0;
            var levelB = buttonB.puzzlePieceHolder?.PuzzlePieceSO?.puzzlePieceLevel ?? 0;

            return levelA.CompareTo(levelB);
        });

        for (int i = 0; i < children.Length; i++)
            children[i].SetSiblingIndex(i);

        Debug.Log("Tri effectué uniquement par niveau.");
    }

    public void SortPiecesByName()
    {
        if (generatorsContent == null) return;

        Transform[] children = new Transform[generatorsContent.transform.childCount];
        for (int i = 0; i < generatorsContent.transform.childCount; i++)
            children[i] = generatorsContent.transform.GetChild(i);

        System.Array.Sort(children, (a, b) =>
        {
            var buttonA = a.GetComponent<BuildMenuButtonPP>();
            var buttonB = b.GetComponent<BuildMenuButtonPP>();
            if (buttonA == null || buttonB == null) return 0;

            string nameA = buttonA.puzzlePieceHolder?.PuzzlePieceSO?.puzzlePieceName ?? "";
            string nameB = buttonB.puzzlePieceHolder?.PuzzlePieceSO?.puzzlePieceName ?? "";

            return string.Compare(nameA, nameB);
        });

        for (int i = 0; i < children.Length; i++)
            children[i].SetSiblingIndex(i);

        Debug.Log("Tri effectué uniquement par nom.");
    }
}



