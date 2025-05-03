using UnityEngine;

public class Cell : MonoBehaviour
{
    public void ActivateCell()
    {
        foreach (Transform turrel in transform)
        {
            if (turrel.gameObject.activeSelf && turrel.TryGetComponent<Turrel>(out var isTurrel))
            {
                isTurrel.Activate();
            }
        }
    }

    public void DiactivateCell()
    {
        foreach (Transform turrel in transform)
        {
            if (turrel.gameObject.activeSelf && turrel.TryGetComponent<Turrel>(out var isTurrel))
            {
                isTurrel.Diactivate();
            }
        }
    }
}
