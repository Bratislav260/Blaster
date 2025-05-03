using UnityEngine;

public interface IDamangeable
{
    float maxHealth { get; set; }

    public void GetDamage(float damage);
}
