using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class SimpleCameraShakeInCinemachine : MonoBehaviour
{

    [SerializeField] float ShakeDuration = 0.7f;
    [SerializeField] float ShakeAmplitude = 1;
    [SerializeField] float ShakeFrequency = 2.0f;

    private float ShakeElapsedTime = 0f;

    [SerializeField] CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    private void Awake()
    {
        if (Instance)
            Destroy(this);

        Instance = this;
    }

    void Start()
    {
        // Obtiene el noise
        if (VirtualCamera != null)
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    // Método para empezar a agitar la cámara
    public void Shake()
    {
        ShakeElapsedTime = ShakeDuration;
    }

    void Update()
    {
        // Si es nulo no haga nada
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            // Si el shake está ejecutándose
            if (ShakeElapsedTime > 0)
            {
                // Poner los parámetros
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                // Actualiza el contador
                ShakeElapsedTime -= Time.fixedUnscaledDeltaTime;
            }
            else
            {
                // Si se terminó resetear las variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }
    }

    public static SimpleCameraShakeInCinemachine Instance { get; private set; }
}
