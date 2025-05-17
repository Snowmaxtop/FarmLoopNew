using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private int width = 5;    // Number of columns
    [SerializeField] private int height = 5;   // Number of rows
    [SerializeField] private float spacing = 1f; // Distance between cells
    [SerializeField] private int AdditionalRow = 0; // Distance between cells
    

    public List<GameObject> allCells = new List<GameObject>();
    public List<GameObject> allObstacles = new List<GameObject>();
    public GameObject TownHall;
    public GameObject Tree;
    public GameObject Stone;

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        GenerateCell();
        GenerateExtraLayers(AdditionalRow);
        GenerateTownHall();
    }

    private void GenerateCell()
    {
        int xOffset = width / 2;
        int yOffset = height / 2;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Compute grid position centered around (0,0)
                Vector2 position = new Vector2((x - xOffset) * spacing, (y - yOffset) * spacing);

                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                cell.name = $"Cell ({x - xOffset}, {y - yOffset})";
                cell.GetComponent<GridCell>().gridPosition = new Vector2Int(x - xOffset, y - yOffset);

                allCells.Add(cell);
            }
        }
    }

    public void GenerateExtraLayers(int numberOfLayers)
    {
        HashSet<Vector2Int> existingPositions = new HashSet<Vector2Int>();
        foreach (GameObject cell in allCells)
        {
            Vector2Int pos = cell.GetComponent<GridCell>().gridPosition;
            existingPositions.Add(pos);
        }

        int xOffset = width / 2;
        int yOffset = height / 2;

        for (int layer = 1; layer <= numberOfLayers; layer++)
        {
            for (int x = -xOffset - layer; x <= xOffset + layer; x++)
            {
                for (int y = -yOffset - layer; y <= yOffset + layer; y++)
                {
                    bool isEdge = x == -xOffset - layer || x == xOffset + layer ||
                                  y == -yOffset - layer || y == yOffset + layer;

                    Vector2Int pos = new Vector2Int(x, y);

                    if (isEdge && !existingPositions.Contains(pos))
                    {
                        Vector2 position = new Vector2(x * spacing, y * spacing);
                        GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                        GridCell currentGridCell = cell.GetComponent<GridCell>();
                        cell.name = $"Cell ({x}, {y})";
                        currentGridCell.gridPosition = pos;
                        currentGridCell.cellState = GridCell.CellState.Obstructed;
                        allCells.Add(cell);
                        existingPositions.Add(pos);

                        // 70% Tree, 30% Stone
                        float chance = Random.value;
                        if (chance < 0.7f && Tree != null)
                        {
                            GameObject currentTree = Instantiate(Tree, cell.transform.position + Tree.transform.position, Quaternion.identity, cell.transform);
                            currentGridCell.obstacle = GridCell.Obstacle.Tree;
                            currentGridCell.holdPuzzlePiece = currentTree;
                            currentTree.GetComponent<RemoveObstacles>().cellHolder = currentGridCell;
                            allObstacles.Add(currentTree);
                        }
                        else if (Stone != null)
                        {
                            GameObject currentStone = Instantiate(Stone, cell.transform.position + Stone.transform.position, Quaternion.identity, cell.transform);
                            currentGridCell.obstacle = GridCell.Obstacle.Stone;
                            
                            currentGridCell.holdPuzzlePiece = currentStone;
                            currentStone.GetComponent<RemoveObstacles>().cellHolder = currentGridCell;
                            allObstacles.Add(currentStone);
                        }
                    }
                }
            }
        }
    }



    private void GenerateTownHall()
    {
        GameObject centerCell = GetCellAt(new Vector2Int(0, 0));
        GameObject townHall = Instantiate(TownHall, centerCell.transform.position + TownHall.transform.position, Quaternion.identity, transform);
        centerCell.GetComponent<GridCell>().cellState = GridCell.CellState.Occupied;
        centerCell.GetComponent<GridCell>().holdPuzzlePiece = townHall;
    }

    public GameObject GetCellAt(Vector2Int coords)
    {
        foreach (GameObject cell in allCells)
        {
            if (cell.GetComponent<GridCell>().gridPosition == coords)
                return cell.gameObject;
        }

        Debug.LogWarning($"No cell found at {coords}");
        return null;
    }

}
