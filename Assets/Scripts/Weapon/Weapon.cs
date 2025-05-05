using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected float magazinMax;
    public float magazinCurrent;
    protected bool isAvableAttack = true;
    protected bool isPlayer = false;
    public WeaponInfo weaponInfo;
    protected Transform firePoint;

    // private void Awake()
    // {
    //     if (IsThisPlayer())
    //     {
    //         isPlayer = true;
    //     }
    //     else
    //     {
    //         isPlayer = false;
    //     }
    // }

    public virtual void Initialize()
    {
        firePoint = transform.GetChild(0);
        magazinMax = weaponInfo.GetStat(WeaponStats.magazin);
        magazinCurrent = magazinMax;
        IsThisPlayer();
    }

    public virtual void Attack()
    {
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

    private void IsThisPlayer()
    {
        Transform grandparent = transform.parent?.parent;

        if (grandparent != null && grandparent.TryGetComponent<Player>(out var player))
        {
            isPlayer = true;
        }
        else
            isPlayer = false;
    }

    #region - Enable / Disable -
    private void OnEnable()
    {
        isAvableAttack = true;
    }
    #endregion
}
