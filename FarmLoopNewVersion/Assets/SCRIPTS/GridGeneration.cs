using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private int width = 5;    // Number of columns
    [SerializeField] private int height = 5;   // Number of rows
    [SerializeField] private float spacing = 1f; // Distance between cells
    [SerializeField] private float AdditionalRow = 0f; // Distance between cells
    

    public List<GameObject> allCells = new List<GameObject>();
    public GameObject TownHall;

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        GenerateCell();
        GenerateExtraLayers(2);
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
                        cell.name = $"Cell ({x}, {y})";
                        cell.GetComponent<GridCell>().gridPosition = pos;

                        allCells.Add(cell);
                        cell.GetComponent<GridCell>().cellState = GridCell.CellState.Obstructed;
                        existingPositions.Add(pos);
                    }
                }
            }
        }
    }


    private void GenerateTownHall()
    {
        GameObject centerCell = GetCellAt(new Vector2Int(0, 0));
        Instantiate(TownHall, centerCell.transform.position + TownHall.transform.position, Quaternion.identity, transform);
        centerCell.GetComponent<GridCell>().cellState = GridCell.CellState.Occupied;
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
