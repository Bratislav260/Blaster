using UnityEngine;

public class CellController : MonoBehaviour
{
    [SerializeField] private Cell[] cells;

    public void Initialize()
    {
        foreach (var cell in cells)
        {
            cell.gameObject.SetActive(false);
        }
    }

    // private void Awake()
    // {
    //     if (cells.Length > 0)
    //     {
    //         cells[0].gameObject.SetActive(true);
    //         cells[0].ActivateCell();
    //     }
    // }

    public void ActivateCurrentCell(int cellIndex)
    {
        cells[cellIndex].gameObject.SetActive(true);
        cells[cellIndex].ActivateCell();
    }

    public void DiactivateCurrentCell(int cellIndex)
    {
        cells[cellIndex].DiactivateCell();
        cells[cellIndex].gameObject.SetActive(false);
    }
}
