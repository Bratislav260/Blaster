using UnityEngine;

public class RetreatState : EnemyState
{
    public RetreatState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        // Debug.Log("ENTER --- RETREAT");
    }

    public override void UpdateState()
    {
        if (enemy.locator.Locate(enemy.chaseRadius))
        {
            enemy.enemyMovement.Move(enemy.locator.GetTargetDirection(false));
        }

        enemy.enemyAI.TurnAttackState();
        enemy.enemyAI.TurnMeleeState();
    }

    public override void ExitState()
    {
        base.ExitState();
        // Debug.Log("EXIT --- RETREAT");

        enemy.enemyMovement.Move(Vector2.zero);
    }
}
