using UnityEngine;

public class ChaseState : EnemyState
{
    public ChaseState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        // Debug.Log("ENTER --- CHASE");
    }

    public override void FixedUpdateState()
    {
        if (enemy.locator.Locate(enemy.chaseRadius))
        {
            enemy.enemyMovement.Move(enemy.locator.GetTargetDirection());
        }

        enemy.enemyAI.TurnIdleState();
        enemy.enemyAI.TurnAttackState();
    }

    public override void ExitState()
    {
        base.ExitState();
        // Debug.Log("EXIT --- CHASE");

        enemy.enemyMovement.Move(Vector2.zero);
    }
}
