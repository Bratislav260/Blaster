using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    public static UnityEvent onMoving = new UnityEvent();
    public static void IsMoved()
    {
        onMoving?.Invoke();
    }

    public static UnityEvent onWeaponRotate = new UnityEvent();
    public static void IsWeaponRotated()
    {
        onWeaponRotate?.Invoke();
    }
}
