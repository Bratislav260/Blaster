using UnityEngine;

public class WeaponSwitch
{
    public int weaponIndex = 0;
    private Player player;

    public WeaponSwitch(Player player)
    {
        this.player = player;
    }

    private void MouseSrollingSwitch(bool isDown = false)
    {
        int currentWeaponIndex = weaponIndex;

        if (isDown)
        {
            weaponIndex--;

            if (weaponIndex < 0)
            {
                weaponIndex = player.inventory.weapons.Count - 1;
            }
        }
        else
        {
            weaponIndex++;

            if (weaponIndex > player.inventory.weapons.Count - 1)
            {
                weaponIndex = 0;
            }
        }

        if (currentWeaponIndex != weaponIndex)
        {
            SelectWeapon(player);
        }
    }

    private void SelectWeapon(Player player)
    {
        int i = 0;
        foreach (Weapon weapon in player.inventory.weapons)
        {
            if (i == weaponIndex)
            {
                weapon.gameObject.SetActive(true);
                player.inventory.SetGun(weapon);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    public void SwitchWeapon(float mouseScrollY)
    {
        if (mouseScrollY > 0)
        {
            MouseSrollingSwitch();
        }
        else if (mouseScrollY < 0)
        {
            MouseSrollingSwitch(true);
        }
    }
}
