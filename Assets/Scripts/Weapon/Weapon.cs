using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected float magazinMax;
    public float magazinCurrent;
    protected bool isAvableAttack = true;
    public WeaponInfo weaponInfo;
    protected Transform firePoint;

    public virtual void Initialize()
    {
        // rechargeAnimation = transform.GetComponentInParent<RechargeAnimation>();
        firePoint = transform.GetChild(0);
        magazinMax = weaponInfo.GetStat(WeaponStats.magazin);
        magazinCurrent = magazinMax;
    }

    public virtual void Attack()
    {
        Debug.Log("IT'S ATTACK");
        EndAttack();
    }

    public virtual void EndAttack() { }

    public virtual void Recharge()
    {
        magazinCurrent = magazinMax;
        isAvableAttack = true;
        UIEventManager.BulletUIUpdate(magazinCurrent);
    }

    public virtual IEnumerator Cooldown()
    {
        isAvableAttack = false;
        yield return new WaitForSeconds(weaponInfo.GetStat(WeaponStats.cooldown));
        isAvableAttack = true;
    }

    #region - Enable / Disable -
    private void OnEnable()
    {
        isAvableAttack = true;
    }
    #endregion
}
