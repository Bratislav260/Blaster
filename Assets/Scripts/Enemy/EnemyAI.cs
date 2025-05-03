using UnityEngine;

public class EnemyAI
{
    private Enemy enemy;

    #region --- State Machine ---
    public StateMachine stateMachine { get; private set; }
    public IdleState IdleState { get; private set; }
    public ChaseState ChaseState { get; private set; }
    public AttackState AttackState { get; private set; }
    public RetreatState RetreatState { get; private set; }
    public MeleeState MeleeState { get; private set; }
    #endregion

    public EnemyAI(Enemy enemy)
    {
        this.enemy = enemy;
        StatesInitialize();
    }

    private void StatesInitialize()
    {
        stateMachine = new StateMachine();
        IdleState = new IdleState(enemy, stateMachine);
        ChaseState = new ChaseState(enemy, stateMachine);
        AttackState = new AttackState(enemy, stateMachine);
        RetreatState = new RetreatState(enemy, stateMachine);
        MeleeState = new MeleeState(enemy, stateMachine);
    }

    public void TurnIdleState()
    {
        if (!enemy.locator.Locate(enemy.chaseRadius))
        {
            stateMachine.ChangeState(IdleState);
        }
    }

    public void TurnChaseState()
    {
        if (enemy.locator.Locate(enemy.chaseRadius, enemy.attackRadius))
        {
            stateMachine.ChangeState(ChaseState);
        }
    }

    public void TurnRetreatState(bool isParallelTurn = false)
    {
        if (enemy.locator.Locate(enemy.retreatRadius, enemy.meleeRadius))
        {
            if (!isParallelTurn)
                stateMachine.ChangeState(RetreatState);
            else
                stateMachine.ParallelState(RetreatState);
        }
    }

    public void TurnAttackState()
    {
        if (enemy.locator.Locate(enemy.attackRadius, enemy.retreatRadius))
        {
            stateMachine.ChangeState(AttackState);
        }
    }

    public void TurnMeleeState()
    {
        if (enemy.locator.Locate(enemy.meleeRadius))
        {
            stateMachine.ChangeState(MeleeState);
        }
    }
}
