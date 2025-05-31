using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PuzzlePiece", menuName = "Scriptable Objects/PuzzlePiece")]
public class PuzzlePieceSO : ScriptableObject
{
    public enum entryDirectionEnum { Left, Right, Top, Down }
    public entryDirectionEnum entryDirection;
    private enum exitDirectionEnum { Left, Right, Top, Down }
    public entryDirectionEnum exitDirection;

    public enum puzzlePieceTypeEnum { Generator, Convertor, Decoration }
    public puzzlePieceTypeEnum PuzzlePieceType;

    public int puzzlePieceLevel = 1;
    public String puzzlePieceName;

    public ItemData itemGenerated;
    public float processTime;

    public Sprite directionSprite;
    public Sprite puzzlePieceIcon;
}
