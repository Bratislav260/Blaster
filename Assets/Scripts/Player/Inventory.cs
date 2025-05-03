using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [field: SerializeField] public List<Weapon> weapons { get; private set; }
    public Weapon currentWeapon { get; private set; }

    public void Initialize()
    {
        foreach (var weapon in weapons)
        {
            weapon.Initialize();
        }
    }

    public void SetGun(Weapon newWeapon)
    {
        currentWeapon = newWeapon;
        UIEventManager.BulletUIUpdate(currentWeapon.magazinCurrent);
    }
}
