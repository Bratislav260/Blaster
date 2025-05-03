using System.Collections.Generic;
using UnityEngine;

public class Soldier : Enemy
{
    #region --- Подклассы Enemy ---
    public WeaponRotater weaponRotater { get; private set; }
    public MeleeController meleeController { get; private set; }
    public UnitAttackController attackController { get; private set; }
    #endregion

    #region --- Параметры ---
    public Transform weaponPivot { get; private set; }
    public SpriteRenderer weaponSpriteRenderer { get; private set; }
    public Weapon weapon { get; private set; }
    #endregion

    public override void Initialize()
    {
        base.Initialize();

        weaponPivot = transform.GetChild(0);
        weapon = transform.GetComponentInChildren<Weapon>();
        weapon.Initialize();
        weaponSpriteRenderer = weapon.GetComponent<SpriteRenderer>();

        enemyMovement = GetComponent<UnitMovement>();
        SetIdling();

        attackController = GetComponent<UnitAttackController>();
        weaponRotater = new WeaponRotater(spriteRenderer);
        meleeController = GetComponent<MeleeController>();
        meleeController.Initialize(weaponPivot);

        // EnemyController.Instance.Add(this);
    }

    private void SetIdling()
    {
        List<Vector2> directions = new List<Vector2>()
        {
            Vector2.right,
            Vector2.zero,
            Vector2.left,
        };
        enemyIdling.Initialize(this, directions);
        enemyAI.stateMachine.Initialize(enemyAI.IdleState);
    }

    public override void EnemyUpdate()
    {
        base.EnemyUpdate();

        if (!isAnimationTrue && locator.Locate(chaseRadius))
        {
            Transform target = locator.GetTarget();
            weaponRotater.WeaponRotate(target.position, weaponPivot, weapon.transform);
        }
    }

    public override void Attack()
    {
        if (meleeController.IsMelee())
        {
            meleeController.MeleeAttack();
        }
        else
        {
            attackController.Attack(weapon);
        }
    }
}
