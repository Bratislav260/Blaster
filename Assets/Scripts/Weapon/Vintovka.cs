using System.Collections;
using UnityEngine;

public class Vintovka : Weapon
{
    public override void Initialize()
    {
        firePoint = transform.GetChild(0);
    }

    public override void Attack()
    {
        if (isAvableAttack)
        {
            Bullet bullet = Instantiate(weaponInfo.bullet, firePoint.position, firePoint.rotation);
            ParticleManager.Instance.CallPartical("Sparks", firePoint);
            bullet.Shooted(transform.right, weaponInfo.GetStat(WeaponStats.damage), weaponInfo.GetStat(WeaponStats.shootSpeed));

            isAvableAttack = false;
            StartCoroutine(Cooldown());

            SoundSystem.Instance.Sound("RifleShot").Play();
        }
    }
}
