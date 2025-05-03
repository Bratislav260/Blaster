using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TeleportController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private List<Collider2D> confinerCollider;
    [SerializeField] private CinemachineConfiner2D confiner;
    [SerializeField] private CellController cellController;
    public PointsList cellsList = new PointsList();
    private Coroutine coroutine;
    private float currentCell = -1;
    [SerializeField] private float waitDuration;
    [SerializeField] private int timerStartTime = 3;

    public void StartTeleporting()
    {
        coroutine = StartCoroutine(RandomTeleport());
    }

    public void StopTeleporting()
    {
        StopCoroutine(coroutine);
    }

    private IEnumerator RandomTeleport()
    {
        while (true)
        {
            int cellIndex = GetRandomCell();
            int posIndex = GetCellPosition(cellsList.pointsList[cellIndex].points);
            // Debug.Log($"{cellIndex} : {posIndex} ");

            player.transform.position = cellsList.pointsList[cellIndex].points[posIndex].position;
            confiner.m_BoundingShape2D = confinerCollider[cellIndex];
            EnemyController.Instance.SetCurrentList(cellIndex);
            cellController.ActivateCurrentCell(cellIndex);

            yield return new WaitForSeconds(waitDuration - timerStartTime);
            UIEventManager.TimerUI();
            yield return new WaitForSeconds(timerStartTime);
            cellController.DiactivateCurrentCell(cellIndex);
            player.playerAnimation.StartTeleportUI(0.4f);
        }
    }

    private int GetRandomCell()
    {
        int rndIndex = Random.Range(0, cellsList.Count());

        if (rndIndex == currentCell)
        {
            rndIndex = rndIndex + 1 > cellsList.Count() - 1 ? 0 : rndIndex + 1;
            currentCell = rndIndex;
            return rndIndex;
        }
        else
        {
            currentCell = rndIndex;
            return rndIndex;
        }
    }

    private int GetCellPosition(List<Transform> cell)
    {
        int rndPosIndex = Random.Range(0, cell.Count);
        return rndPosIndex;
    }
}
