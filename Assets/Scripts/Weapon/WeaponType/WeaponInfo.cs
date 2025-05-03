using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Weapon")]
public class WeaponInfo : ScriptableObject
{
    [SerializedDictionary("Key", "Value")] public SerializedDictionary<WeaponStats, float> weaponStat;
    [field: SerializeField] public Bullet bullet { get; private set; }

    public float GetStat(WeaponStats stat)
    {
        if (weaponStat.TryGetValue(stat, out float value))
        {
            return value;
        }
        else
        {
            Debug.LogError($"Нет такого STAT: {stat}");
            return 0f;
        }
    }
}

public enum WeaponStats
{
    damage,
    shootSpeed,
    cooldown,
    recharge,
    magazin
}