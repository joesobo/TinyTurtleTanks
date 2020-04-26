using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[ExecuteInEditMode]
public class CameraShake : CinemachineExtension
{
    [Tooltip("Amplitude of the shake")]
    public float m_Range = 0.5f;

    public float shakeTiming = 0.5f;

    public bool toggleShake = false;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (toggleShake)
        {
            if (stage == CinemachineCore.Stage.Body)
            {
                Vector3 shakeAmount = GetOffset();
                state.PositionCorrection += shakeAmount;
            }
        }
    }

    public IEnumerator Shake()
    {
        toggleShake = true;
        yield return new WaitForSeconds(shakeTiming);
        toggleShake = false;
    }

    Vector3 GetOffset()
    {
        float x = Random.Range(-1, 1) * m_Range;
        float y = Random.Range(-1, 1) * m_Range;
        return new Vector3(x, y, 0);
    }
}
