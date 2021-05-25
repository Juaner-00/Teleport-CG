using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class SimpleCameraShakeInCinemachine : MonoBehaviour
{
    [SerializeField] float shakeAmplitude = 1;
    [SerializeField] float shakeFrequency = 2.0f;

    [SerializeField] CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    public float ShakeAmplitude { get => shakeAmplitude; set => shakeAmplitude = value; }

    // private void Awake()
    // {
    //     if (Instance)
    //         Destroy(this);

    //     Instance = this;
    // }

    void Start()
    {
        // Obtiene el noise
        if (VirtualCamera != null)
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        // Si es nulo no haga nada
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            // Poner los parámetros
            virtualCameraNoise.m_AmplitudeGain = shakeAmplitude * 1.5f;
            virtualCameraNoise.m_FrequencyGain = shakeFrequency;
        }
    }

    // public static SimpleCameraShakeInCinemachine Instance { get; private set; }
}
