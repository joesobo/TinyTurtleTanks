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
        // Note: change this to something more interesting!
        return new Vector3(
            Random.Range(-m_Range, m_Range),
            Random.Range(-m_Range, m_Range),
            Random.Range(-m_Range, m_Range));
    }
}
