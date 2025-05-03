using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        // Debug.Log("ENTER --- ATTACK");
    }

    public override void UpdateState()
    {
        enemy.Attack();
        enemy.enemyAI.TurnChaseState();
        enemy.enemyAI.TurnRetreatState(isParallelTurn: true);
    }

    public override void ExitState()
    {
        base.ExitState();
        // Debug.Log("EXIT --- ATTACK");
    }
}
