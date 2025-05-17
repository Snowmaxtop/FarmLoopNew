using UnityEngine;

public class GridCell : MonoBehaviour
{

    public Vector2Int gridPosition;
    public GameObject holdPuzzlePiece;


    public enum CellState {Empty, Occupied, Obstructed}
    public CellState cellState = CellState.Empty;
   
    public enum Obstacle {Tree, Stone}
    public Obstacle obstacle = Obstacle.Tree;

    [Header("Removal Costs")]
    public int shovelCost;
    public int dynamiteCost;

    private void Start()
    {
        // Optional: Set default costs based on type
        switch (obstacle)
        {
            case Obstacle.Tree:
                shovelCost = 1;
                dynamiteCost = 0;
                break;
            case Obstacle.Stone:
                shovelCost = 2;
                dynamiteCost = 1;
                break;
        }
    }
}
