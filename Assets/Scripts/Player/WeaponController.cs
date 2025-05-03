using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private new Camera camera;
    private Vector3 mousePos;
    private Player player;
    private WeaponRotater weaponRotater;

    public void Initialize(Player player)
    {
        camera = Camera.main;
        weaponRotater = new WeaponRotater(player.spriteRenderer);
        this.player = player;
        SetStartWeapon();
    }

    public void WeaponRotate()
    {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        weaponRotater.WeaponRotate(mousePos, player.weaponPivot, player.inventory.currentWeapon.transform);

        if (player.weaponPivot.rotation.x != weaponRotater.previousAngle)
        {
            EventManager.IsWeaponRotated();
        }
    }

    private void SetStartWeapon()
    {
        if (player.weaponPivot.childCount > 0)
        {
            foreach (Transform weaponTra in player.weaponPivot)
            {
                weaponTra.gameObject.SetActive(false);
            }

            if (player.weaponPivot.GetChild(0).TryGetComponent<Weapon>(out var weapon))
            {
                player.inventory.SetGun(weapon);
                player.weaponPivot.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
