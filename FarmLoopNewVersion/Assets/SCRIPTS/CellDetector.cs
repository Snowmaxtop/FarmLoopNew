using UnityEngine;

public class CellDetector : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                GridCell cell = clickedObject.GetComponent<GridCell>();

                if (cell != null)
                {
                    
                    Debug.Log($"Clicked on cell at position: {cell.gridPosition}");
                }
                else
                {
                    //Fermer le DestroyUI de toutes les cells.
                    //Activer le DestroyUI de la cell en question

                    

                    clickedObject.GetComponent<RemoveObstacles>().ActivateDestroyUI();
                    Debug.Log($"Clicked on an obstacle at position:" +  clickedObject.GetComponent<RemoveObstacles>().cellHolder.gridPosition.ToString());
                }
            }
            else
            {
                Debug.Log("No object was clicked.");

                foreach (GameObject obstacles in GridManager.Instance._gridGenerator.allObstacles)
                {
                    obstacles.GetComponent<RemoveObstacles>().DesactivateDestroyUI();
                }
            }
        }
    }
}
