using UnityEngine;
using UnityEngine.Events;

public class UIEventManager
{
    public static UnityEvent<int> onDiying = new UnityEvent<int>();
    public static void HeartUIUpdate(int hearts)
    {
        onDiying?.Invoke(hearts);
    }

    public static UnityEvent<float> onShooted = new UnityEvent<float>();
    public static void BulletUIUpdate(float bulllet)
    {
        onShooted?.Invoke(bulllet);
    }

    public static UnityEvent<float> onHeal = new UnityEvent<float>();
    public static void HealbarUIUpdate(float health)
    {
        onHeal?.Invoke(health);
    }

    public static UnityEvent onTimer = new UnityEvent();
    public static void TimerUI()
    {
        onTimer?.Invoke();
    }
}
