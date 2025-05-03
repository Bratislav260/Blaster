using UnityEngine;

public class WeaponRotater
{
    private SpriteRenderer unitSpriteRenderer;
    public float previousAngle { get; private set; } = 0;

    public WeaponRotater(SpriteRenderer spriteRenderer)
    {
        unitSpriteRenderer = spriteRenderer;
    }

    public void WeaponRotate(Vector3 target, Transform weaponPivot, Transform currentWeapon)
    {
        Vector2 lookDirection = target - weaponPivot.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        previousAngle = angle;

        weaponPivot.rotation = Quaternion.Euler(0, 0, angle);

        if (Mathf.Abs(angle) > 90)
        {
            currentWeapon.GetComponent<SpriteRenderer>().flipY = true;
            unitSpriteRenderer.flipX = true;
        }
        else if (Mathf.Abs(angle) < 90)
        {
            currentWeapon.GetComponent<SpriteRenderer>().flipY = false;
            unitSpriteRenderer.flipX = false;
        }
    }
}
