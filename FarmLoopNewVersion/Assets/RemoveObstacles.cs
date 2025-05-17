using TMPro;
using UnityEngine;

public class RemoveObstacles : MonoBehaviour
{
    public GridCell cellHolder;
    public GameObject obstacleUI;
    public TextMeshProUGUI shovelCost;
    public TextMeshProUGUI dynamiteCost;

    public void ActivateDestroyUI()
    {
        foreach(GameObject obstacles in GridManager.Instance._gridGenerator.allObstacles)
        {
            obstacles.GetComponent<RemoveObstacles>().DesactivateDestroyUI();
        }

        shovelCost.text = cellHolder.shovelCost.ToString();
        dynamiteCost.text = cellHolder.dynamiteCost.ToString();
        obstacleUI.SetActive(true);
    }

    public void DesactivateDestroyUI()
    {
        obstacleUI.SetActive(false);
    }

    public void DestroyObstacle()
    {
        if (cellHolder.obstacle == GridCell.Obstacle.Tree) {

            if (InventoryManager.Instance.shovelInventoryAmount >= cellHolder.shovelCost && InventoryManager.Instance.dynamiteInventoryAmount >= cellHolder.dynamiteCost)
            {
                CanvasManager.Instance.updateShovelCount(InventoryManager.Instance.shovelInventoryAmount - cellHolder.shovelCost);
                CanvasManager.Instance.updateDynamiteCount(InventoryManager.Instance.dynamiteInventoryAmount - cellHolder.dynamiteCost);
                cellHolder.holdPuzzlePiece = null;
                GridManager.Instance._gridGenerator.allObstacles.Remove(this.gameObject);
                Destroy(this.gameObject);

            }
        }
        else
        {
            if (InventoryManager.Instance.shovelInventoryAmount >= cellHolder.shovelCost && InventoryManager.Instance.dynamiteInventoryAmount >= cellHolder.dynamiteCost)
            {
                CanvasManager.Instance.updateShovelCount(InventoryManager.Instance.shovelInventoryAmount - cellHolder.shovelCost);
                CanvasManager.Instance.updateDynamiteCount(InventoryManager.Instance.dynamiteInventoryAmount - cellHolder.dynamiteCost);
                cellHolder.holdPuzzlePiece = null;
                GridManager.Instance._gridGenerator.allObstacles.Remove(this.gameObject);
                Destroy(this.gameObject);

            }
        }

    }

}
