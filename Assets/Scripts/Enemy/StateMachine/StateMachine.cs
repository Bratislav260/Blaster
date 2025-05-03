using System;

public class StateMachine
{
    public EnemyState currentEnemyState;
    public Action currentEnemyStateUpdate;
    public Action currentEnemyStateFixedUpdate;

    public void Initialize(EnemyState enemyState)
    {
        currentEnemyState = enemyState;
        currentEnemyState.EnterState();
        RegisterAction();
    }

    public void ChangeState(EnemyState newEnemyState)
    {
        currentEnemyState.ExitState();
        currentEnemyState = newEnemyState;
        RegisterAction();
        currentEnemyState.EnterState();
    }

    public void ParallelState(EnemyState newEnemyState)
    {
        currentEnemyState.ExitState();
        currentEnemyState = newEnemyState;
        RegisterParallelAction(newEnemyState);
        currentEnemyState.EnterState();
    }

    public void ExitState()
    {
        currentEnemyState.ExitState();
    }

    private void RegisterAction()
    {
        currentEnemyStateUpdate = currentEnemyState.UpdateState;
        currentEnemyStateFixedUpdate = currentEnemyState.FixedUpdateState;
    }

    private void RegisterParallelAction(EnemyState newEnemyState)
    {
        currentEnemyStateUpdate += newEnemyState.UpdateState;
        currentEnemyStateFixedUpdate += newEnemyState.FixedUpdateState;
    }
}
