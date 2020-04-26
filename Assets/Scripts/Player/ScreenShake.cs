using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
    public float shakeIntensity = 5f;
    public float amplitudeGain = 1f;
    public float shakeTiming = 0.5f;

    public CinemachineFreeLook cmFreeCam;

    // public IEnumerator Shake(float duration, float magnitude)
    // {
    //     Vector3 originalPos = transform.localPosition;

    //     float elapsed = 0f;
    //     while (elapsed < duration)
    //     {
    //         float x = Random.Range(-1, 1) * magnitude;
    //         float y = Random.Range(-1, 1) * magnitude;

    //         transform.localPosition = new Vector3(x, y, originalPos.z);

    //         elapsed += Time.deltaTime;

    //         yield return null;
    //     }

    //     transform.localPosition = originalPos;
    // }

    public IEnumerator Shake()
    {
        Noise(amplitudeGain, shakeIntensity);
        yield return new WaitForSeconds(shakeTiming);
        Noise(0, 0);
    }

    public void Noise(float amplitudeGain, float frequencyGain)
    {
        cmFreeCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitudeGain;

        cmFreeCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = frequencyGain;
    }
}
