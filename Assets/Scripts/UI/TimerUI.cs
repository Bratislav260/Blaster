using System.Collections;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private int timerTime = 3;
    [SerializeField] private int currentTimerTime;

    public void Initialize()
    {
        UIEventManager.onTimer.AddListener(StartTimer);
        timer.gameObject.SetActive(false);
        currentTimerTime = timerTime;
    }

    public void StartTimer()
    {
        timer.gameObject.SetActive(true);
        StartCoroutine(Timer());
    }

    public IEnumerator Timer()
    {
        while (currentTimerTime > -1)
        {
            timer.text = currentTimerTime.ToString();
            SoundSystem.Instance.Sound("TimerTick").Play();
            currentTimerTime -= 1;
            yield return new WaitForSeconds(1f);
        }

        timer.gameObject.SetActive(false);
        currentTimerTime = timerTime;
        yield break;
    }
}
