using System.Collections.Generic;
using UnityEngine;

public class HeartUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> heartsList;

    public void Initialize()
    {
        UIEventManager.onDiying.AddListener(UIHeartUpdate);
    }

    public void UIHeartUpdate(int hearts)
    {
        heartsList[hearts].SetActive(false);
    }
}