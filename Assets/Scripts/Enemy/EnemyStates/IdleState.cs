using UnityEngine;

public class IdleState : EnemyState
{
    public IdleState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        // Debug.Log("ENTER --- IDLE");
        enemy.enemyIdling.StartIdling();
    }

    public override void FixedUpdateState()
    {
        enemy.enemyIdling.Idling();
        enemy.enemyAI.TurnChaseState();
        enemy.enemyAI.TurnAttackState();
        enemy.enemyAI.TurnRetreatState();
    }

    public override void ExitState()
    {
        base.ExitState();
        // Debug.Log("EXIT --- IDLE");
        enemy.enemyIdling.Stopidling();
    }
}
