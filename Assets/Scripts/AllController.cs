using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllController : MonoBehaviour
{
    [SerializeField] float baseSpeed, baseSize, effect2Delay;
    [SerializeField] GameObject disolver;
    [SerializeField] ParticleSystem fireRing, trailParticle;
    [SerializeField] ProjectorRotation projectorR;
    [SerializeField] Gradient gradient1, gradient2;
    [SerializeField] AnimationCurve curveSpeed;
    [SerializeField] Image flashImage;
    [SerializeField] AnimationCurve flashCurve;
    [SerializeField] GameObject[] gObject;

    ParticleSystem[] allParticles;
    ParticleSystem[] effect1Particles;
    ParticleSystem[] effect2Particles;
    ParticleSystem.MainModule mainP;
    ParticleSystem.ColorOverLifetimeModule colorP;
    ParticleSystem.ShapeModule shapeP;
    ParticleSystem.TrailModule trailP;
    ParticleSystem.EmissionModule emissionP;
    ParticleSystem.VelocityOverLifetimeModule velP;

    public static Action OnEffectFinished;

    float[] delays;
    int lenght;

    float t;
    Gradient gradient;

    bool hasFinised;
    bool effect1Active;
    bool effect2Active;

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        Instance = this;

        allParticles = GetComponentsInChildren<ParticleSystem>();

        effect1Particles = transform.GetChild(0).GetComponentsInChildren<ParticleSystem>();
        effect2Particles = transform.GetChild(1).GetComponentsInChildren<ParticleSystem>();
    }

    private void Start()
    {
        hasFinised = true;

        lenght = effect2Particles.Length;
        delays = new float[lenght];
        for (int i = 0; i < lenght; i++)
        {
            mainP = effect2Particles[i].main;
            delays[i] = mainP.startDelay.Evaluate(0);
        }
    }

    void SetVariables()
    {
        trailP = trailParticle.trails;

        foreach (GameObject gO in gObject)
        {
            gO.transform.localScale = new Vector3(gO.transform.localScale.x * baseSize, gO.transform.localScale.y, gO.transform.localScale.z * baseSize);
        }

        foreach (ParticleSystem p in allParticles)
        {
            velP = p.velocityOverLifetime;
            emissionP = p.emission;
            shapeP = p.shape;
            mainP = p.main;
            colorP = p.colorOverLifetime;
            colorP.color = gradient;
            mainP.startSizeMultiplier *= baseSize;
            shapeP.radius *= baseSize;
            emissionP.rateOverTimeMultiplier *= baseSize;
            velP.speedModifierMultiplier *= baseSize;
        }

        if (effect1Active)
            for (int i = 0; i < lenght; i++)
            {
                mainP = effect2Particles[i].main;
                mainP.startDelay = new ParticleSystem.MinMaxCurve(delays[i] + effect2Delay);
            }

        projectorR.DissolveDuration = projectorR.DissolveDuration / baseSpeed;
        trailP.colorOverLifetime = gradient;

        hasFinised = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasFinised)
            t += Time.deltaTime / 25 * baseSpeed;

        foreach (ParticleSystem p in allParticles)
        {
            mainP = p.main;
            mainP.simulationSpeed = baseSpeed * (curveSpeed.Evaluate(t));
        }

        if (fireRing.isStopped && !hasFinised)
            disolver.SetActive(true);

        if (effect2Active)
            if (!IsEffect2Playing && !hasFinised)
            {
                hasFinised = true;
                OnEffectFinished?.Invoke();
            }

        if (effect1Active && !effect2Active)
            if (!IsEffect1Playing && !hasFinised)
            {
                hasFinised = true;
                OnEffectFinished?.Invoke();
            }

        // if (IsEffect2Playing && !hasFinised)
        flashImage.color = new Color(flashImage.color.r, flashImage.color.g, flashImage.color.b, flashCurve.Evaluate(t));

    }

    public void Activate(bool ef1, bool ef2, float size, float speed, int color)
    {
        switch (color)
        {
            case 1:
                gradient = gradient1;
                break;
            case 0:
                gradient = gradient2;
                break;
        }

        baseSize = size;
        baseSpeed = speed;

        effect1Active = ef1;
        effect2Active = ef2;

        SetVariables();

        if (effect2Active && !effect1Active)
            t = effect2Delay / 25 * baseSpeed;
        else
            t = 0;

        if (effect1Active)
            foreach (var particle in effect1Particles)
                particle.Play();

        if (effect2Active)
            foreach (var particle in effect2Particles)
                particle.Play();

        hasFinised = false;
    }

    bool IsPlaying
    {
        get
        {
            foreach (ParticleSystem p in allParticles)
                if (p.isPlaying)
                    return true;

            return false;
        }
    }

    bool IsEffect1Playing
    {
        get
        {
            foreach (ParticleSystem p in effect1Particles)

                if (p.isPlaying)
                    return true;

            return false;
        }
    }
    bool IsEffect2Playing
    {
        get
        {
            foreach (ParticleSystem p in effect2Particles)

                if (p.isPlaying)
                    return true;

            return false;
        }
    }

    public static AllController Instance { get; private set; }
}
