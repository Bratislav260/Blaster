using UnityEngine;
using UnityEngine.UI;

public class HealBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    public void Initialize()
    {
        UIEventManager.onHeal.AddListener(UpdateHealthBar);
    }

    public void UpdateHealthBar(float currentHealth)
    {
        healthBar.fillAmount = currentHealth;
    }
}