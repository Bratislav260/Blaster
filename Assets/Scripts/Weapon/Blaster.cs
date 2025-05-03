using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Blaster : Weapon
{
    private LineRenderer lazerLine;
    [SerializeField] private LayerMask ignoreLayer;
    private Vector3 lazerEndPoint;
    private Vector3 newlazerEndPoint;
    [SerializeField] private float lazerSpeed;
    private bool isBlastering = false;

    public override void Initialize()
    {
        base.Initialize();

        lazerLine = GetComponent<LineRenderer>();
        lazerLine.enabled = false;
        EventManager.onWeaponRotate.AddListener(UpdateLazerPosition);
    }

    public override void Attack()
    {
        if (magazinCurrent <= 0)
        {
            Invoke(nameof(Recharge), weaponInfo.GetStat(WeaponStats.recharge));
            SoundSystem.Instance.Sound("NoBatary").Play();

        }
        // Debug.Log("ЛАЗЕРНЫЙ ЛУУУЧ");
        lazerLine.enabled = true;

        newlazerEndPoint = firePoint.position + firePoint.right * 100f;

        RaycastHit2D[] hits = Physics2D.RaycastAll(firePoint.position, firePoint.right, 100f);

        List<IDamangeable> enemies = new List<IDamangeable>();
        foreach (var hit in hits)
        {
            if ((ignoreLayer.value & (1 << hit.collider.gameObject.layer)) != 0)
            {
                enemies.Add(hit.collider.GetComponent<IDamangeable>());
                continue;
            }

            // lazerEndPoint = hit.point;
            newlazerEndPoint = hit.point;
            break;
        }

        if (!isBlastering)
        {
            lazerEndPoint = newlazerEndPoint;
            isBlastering = true;
        }

        StartCoroutine(LazerMove());
        lazerLine.SetPosition(1, lazerEndPoint);

        LazerDamage(enemies);
        MagazinDecrease();

        if (!SoundSystem.Instance.Sound("Blaster").isPlaying)
        {
            SoundSystem.Instance.Sound("Blaster").Play();
        }
    }

    private void LazerDamage(List<IDamangeable> enemies)
    {
        foreach (IDamangeable enemy in enemies)
        {
            if (enemy != null)
                enemy.GetDamage(weaponInfo.GetStat(WeaponStats.damage));
        }
    }

    private void UpdateLazerPosition()
    {
        lazerLine.SetPosition(0, firePoint.position);
    }

    private void MagazinDecrease()
    {
        if (magazinCurrent > 0)
        {
            magazinCurrent -= 0.5f;
        }
        else
        {
            EndAttack();
        }
    }

    public override void EndAttack()
    {
        lazerLine.enabled = false;
        isBlastering = false;
        SoundSystem.Instance.Sound("Blaster").Stop();
    }

    public override void Recharge()
    {
        magazinCurrent = magazinMax;
        UIEventManager.BulletUIUpdate(magazinCurrent);
    }

    private IEnumerator LazerMove()
    {
        while (Vector2.Distance(lazerEndPoint, newlazerEndPoint) > 0.1f)
        {
            lazerEndPoint = Vector2.Lerp(lazerEndPoint, newlazerEndPoint, lazerSpeed * Time.deltaTime);

            if (!lazerLine.enabled)
            {
                yield break;
            }

            yield return null;
        }
    }

    #region - Enable / Disable -
    private void OnDisable()
    {
        EndAttack();
    }
    #endregion
}
