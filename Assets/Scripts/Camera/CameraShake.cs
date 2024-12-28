using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private CinemachineVirtualCamera _virtualCamera;

    private float _shakeTime;
    private void Awake()
    {
        Instance = this;
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
           _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        _shakeTime = time;
    }

    private void Update()
    {
        if (_shakeTime > 0) 
        {
            _shakeTime -= Time.deltaTime;
            if (_shakeTime <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                   _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
        


    }
}
