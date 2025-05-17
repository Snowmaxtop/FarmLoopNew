using UnityEngine;

public class GridCell : MonoBehaviour
{

    public Vector2Int gridPosition;
    public GameObject holdPuzzlePiece;


    public enum CellState {Empty, Occupied, Obstructed}
    public CellState cellState = CellState.Empty;
   
    public enum Obstacle {Tree, Stone}
    public Obstacle obstacle = Obstacle.Tree;


}
