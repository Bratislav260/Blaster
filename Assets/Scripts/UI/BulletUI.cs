using UnityEngine;
using TMPro;

public class BulletUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void Initialize()
    {
        UIEventManager.onShooted.AddListener(BulletUpdate);
    }

    public void BulletUpdate(float bullet)
    {
        text.text = $"{Mathf.Ceil(bullet)}";
    }
}
