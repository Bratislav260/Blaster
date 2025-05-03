using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private HeartUI heartUI;
    [SerializeField] private HealBar healBar;
    [SerializeField] private BulletUI bulletUI;
    [SerializeField] private TimerUI timerUI;

    public void Initialize()
    {
        healBar.Initialize();
        bulletUI.Initialize();
        heartUI.Initialize();
        timerUI.Initialize();
    }
}
