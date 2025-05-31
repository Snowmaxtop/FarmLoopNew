using UnityEngine;
using UnityEngine.UI;

public class BuildMenuButtonPP : MonoBehaviour
{
    public PuzzlePiece puzzlePieceHolder;
    public Image directionAsset;
    public Image ppIcon;

    

    public void PlacePuzzlePiece()
    {
        //WHEN BUTTON IS CLICKED BUILD
    }

    public void UpdateButtonVisuals()
    {
        directionAsset.sprite = puzzlePieceHolder.PuzzlePieceSO.directionSprite;
        ppIcon.sprite = puzzlePieceHolder.PuzzlePieceSO.puzzlePieceIcon;
    }
}
