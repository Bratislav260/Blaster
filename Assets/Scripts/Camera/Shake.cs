using UnityEngine;
using Cinemachine;
using System.Collections;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float startIntensity;

    public void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBMCP =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        startIntensity = intensity;
        cinemachineBMCP.m_AmplitudeGain = intensity;

        StartCoroutine(ShakeTimer(cinemachineBMCP));
    }

    private IEnumerator ShakeTimer(CinemachineBasicMultiChannelPerlin cinemachineBMCP)
    {
        // float elapsedTime = 0;

        while (0 < cinemachineBMCP.m_AmplitudeGain)
        {
            // float time = elapsedTime / duration;
            cinemachineBMCP.m_AmplitudeGain -= 0.2f;


            // elapsedTime += 0.2f;
            yield return null;
        }

        cinemachineBMCP.m_AmplitudeGain = 0;
    }
}
