using System.Collections;
using UnityEngine;

public class Shotgun : Weapon
{
    public override void Attack()
    {
        if (magazinCurrent > 0 && isAvableAttack)
        {
            if (isPlayer)
            {
                CinemachineShake.Instance.ShakeCamera(4f);
            }

            int bulletsAmout = Random.Range(10, 15);

            for (int i = 0; i < bulletsAmout; i++)
            {
                Bullet bullet = Instantiate(weaponInfo.bullet, firePoint.position, firePoint.rotation);
                ParticleManager.Instance.CallPartical("Sparks", firePoint);
                SoundSystem.Instance.Sound("ShotgunShoot").Play();

                float baseAngle = Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
                float randomAngle = baseAngle + Random.Range(-20.25f, 20.25f);
                float radians = randomAngle * Mathf.Deg2Rad;
                Vector2 bulletDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

                bullet.Shooted(bulletDirection, weaponInfo.GetStat(WeaponStats.damage), weaponInfo.GetStat(WeaponStats.shootSpeed), 0.3f);
            }

            magazinCurrent -= 1;
            if (magazinCurrent <= 0)
            {
                isAvableAttack = false;
                Invoke(nameof(Recharge), weaponInfo.GetStat(WeaponStats.recharge));

                SoundSystem.Instance.Sound("ShotgunRecharge").Play();
            }
            else
            {
                StartCoroutine(Cooldown());
            }
        }
    }

    public override IEnumerator Cooldown()
    {
        isAvableAttack = false;
        yield return new WaitForSeconds(weaponInfo.GetStat(WeaponStats.cooldown));
        isAvableAttack = true;
        SoundSystem.Instance.Sound("ShotgunReload").Play();
    }
}
