using UnityEngine;

public class UnitAttackController : MonoBehaviour
{
    public void Attack(Weapon weapon)
    {
        weapon.Attack();
    }
}
