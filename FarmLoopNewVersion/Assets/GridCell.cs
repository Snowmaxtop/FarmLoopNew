using UnityEngine;

public class GridCell : MonoBehaviour
{

    public Vector2Int gridPosition;

    public enum CellState
    {
        Empty,
        Occupied,
        Obstructed
    }

    public CellState cellState = CellState.Empty;

}
