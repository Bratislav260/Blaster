using System.Collections.Generic;
using UnityEngine;

public class Jetpucker : Enemy
{
    #region --- Подклассы Enemy ---
    public UnitAttackController attackController { get; private set; }
    public WeaponRotater weaponRotater { get; private set; }
    public MeleeController meleeController { get; private set; }
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

        SetIdling();

        attackController = GetComponent<UnitAttackController>();
        weaponRotater = new WeaponRotater(spriteRenderer);
        meleeController = GetComponent<MeleeController>();
        meleeController.Initialize(weaponPivot);

        // EnemyController.Instance.Add(this);
    }

    private void SetIdling()
    {
        List<Vector2> directions = GenerateRandomDirections();

        enemyIdling.Initialize(this, directions);
        enemyAI.stateMachine.Initialize(enemyAI.IdleState);
    }

    public override void EnemyUpdate()
    {
        base.EnemyUpdate();

        if (locator.Locate(chaseRadius))
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

    private List<Vector2> GenerateRandomDirections()
    {
        List<Vector2> directions = new List<Vector2>();

        for (int angle = 0; angle < 360; angle += 30)
        {
            float radians = angle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            directions.Add(direction);
        }

        return directions;
    }
}
