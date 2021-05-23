using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingsController : MonoBehaviour
{
    [Header("Star")]
    [SerializeField] Light starLight;

    [Header("Sphere")]
    [SerializeField] ParticleSystem sphereSystem;
    [SerializeField] Light sphereLight;
    [SerializeField, ColorUsage(true, true)] Color starEmission;
    [SerializeField] AnimationCurve curve;

    ParticleSystem.SizeOverLifetimeModule sizeModule;
    ParticleSystem.MainModule mainModule;


    private void Awake()
    {
        sizeModule = sphereSystem.sizeOverLifetime;
    }

    private void Start()
    {
        sizeModule.size = new ParticleSystem.MinMaxCurve(1, curve);
    }


}
