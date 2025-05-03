using UnityEngine;

[RequireComponent(typeof(MeleeController))]
public class AttackController : MonoBehaviour
{
    private Player player;
    private MeleeController meleeController;

    public void Initialize(Player player, Transform weaponPivot)
    {
        this.player = player;

        meleeController = GetComponent<MeleeController>();
        meleeController.Initialize(weaponPivot);
    }

    public void Attack()
    {
        if (meleeController && meleeController.IsMelee())
        {
            meleeController.MeleeAttack();
        }
        else
        {
            player.inventory.currentWeapon.Attack();
            UIEventManager.BulletUIUpdate(player.inventory.currentWeapon.magazinCurrent);
        }
    }

    public void AttackEnd()
    {
        if (!meleeController.IsMelee())
        {
            player.inventory.currentWeapon.EndAttack();
        }
    }
}
