using UnityEngine;

public class MeleeState : EnemyState
{
    public MeleeState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        // Debug.Log("ENTER --- MELEE");
    }

    public override void UpdateState()
    {
        enemy.Attack();
        enemy.enemyAI.TurnRetreatState();
    }

    public override void ExitState()
    {
        base.ExitState();
        // Debug.Log("EXIT --- MELEE");
    }
}
