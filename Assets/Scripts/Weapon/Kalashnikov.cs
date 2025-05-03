using UnityEngine;

public class Kalashnikov : Weapon
{
    public override void Attack()
    {
        if (magazinCurrent > 0 && isAvableAttack)
        {
            // if (isPlayer)
            // {
            //     CinemachineShake.Instance.ShakeCamera(2f);
            // }

            Bullet bullet = Instantiate(weaponInfo.bullet, firePoint.position, firePoint.rotation);
            ParticleManager.Instance.CallPartical("Sparks", firePoint);
            bullet.Shooted(transform.right, weaponInfo.GetStat(WeaponStats.damage), weaponInfo.GetStat(WeaponStats.shootSpeed));
            magazinCurrent -= 1;

            SoundSystem.Instance.Sound("KalashShoot").Play();

            if (magazinCurrent <= 0)
            {
                isAvableAttack = false;
                // rechargeAnimation.StartRechareAnimation(transform.parent, weaponInfo.recharge);

                SoundSystem.Instance.Sound("KalashReload").Play();
                Invoke(nameof(Recharge), weaponInfo.GetStat(WeaponStats.recharge));
            }
            else
            {
                StartCoroutine(Cooldown());
            }
        }
    }
}
