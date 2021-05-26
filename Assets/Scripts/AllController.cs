using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllController : MonoBehaviour
{
    [SerializeField] float baseSpeed, baseSize, effect2Delay;
    [SerializeField] GameObject disolveObject;
    [SerializeField] ParticleSystem fireRing, trailParticle;
    [SerializeField] ProjectorRotation projector;
    [SerializeField] Gradient gradient1, gradient2;
    [SerializeField] AnimationCurve curveSpeed;
    [SerializeField] Image flashImage;
    [SerializeField] AnimationCurve flashCurve;
    [SerializeField] GameObject[] gObject;

    [Header("Controll Curve")]
    [SerializeField] AnimationCurve intensityCurve;
    [SerializeField] SimpleCameraShakeInCinemachine[] shakeControllers;
    [SerializeField] BloomEffect bloom;
    [SerializeField] float bloomMultiplier;
    [SerializeField] AnimationCurve minBloomEffect;

    [SerializeField] AnimationCurve curvePlayer;
    [SerializeField] Material playerMaterial;
    // [SerializeField] CameraShakeController shakeController;

    [Header("Teleport")]
    [SerializeField] GameObject objectToTeleport;
    [SerializeField] Transform posInitial;
    [SerializeField] Transform posFinish;
    [SerializeField] GameObject vCam1;
    [SerializeField] GameObject vCam2;
    [SerializeField] Cinemachine.CinemachineBlenderSettings effect1and2Blend;
    [SerializeField] Cinemachine.CinemachineBlenderSettings onlyEffect2Blend;

    ParticleSystem[] allParticles;
    ParticleSystem[] effect1Particles;
    ParticleSystem[] effect2Particles;
    ParticleSystem.MainModule mainP;
    ParticleSystem.ColorOverLifetimeModule colorP;
    ParticleSystem.ShapeModule shapeP;
    ParticleSystem.TrailModule trailP;
    ParticleSystem.EmissionModule emissionP;
    ParticleSystem.VelocityOverLifetimeModule velP;

    [SerializeField] Animator anim;

    public static Action OnEffectFinished;

    float[] delays;
    int lenght;

    float t;
    Gradient gradient;

    bool hasFinised;
    bool effect1Active;
    bool effect2Active;
    bool teleported;

    bool isEffect1Playing;
    bool isEffect2Playing;

    Cinemachine.CinemachineBrain brain;


    private void OnEnable()
    {
        UIController.OnStartEffect += Activate;
    }

    private void OnDisable()
    {
        UIController.OnStartEffect -= Activate;
    }

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        Instance = this;

        allParticles = GetComponentsInChildren<ParticleSystem>();

        brain = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();

        effect1Particles = transform.GetChild(0).GetComponentsInChildren<ParticleSystem>();
        effect2Particles = transform.GetChild(1).GetComponentsInChildren<ParticleSystem>();
    }

    private void Start()
    {
        hasFinised = true;

        posInitial.position = objectToTeleport.transform.position;

        foreach (var shaker in shakeControllers)
            shaker.ShakeAmplitude = intensityCurve.Evaluate(0);

        lenght = effect2Particles.Length;
        delays = new float[lenght];
        for (int i = 0; i < lenght; i++)
        {
            mainP = effect2Particles[i].main;
            delays[i] = mainP.startDelay.Evaluate(0);
        }

        objectToTeleport.transform.position = posInitial.position;
        objectToTeleport.transform.rotation = posInitial.rotation;

        playerMaterial.SetFloat("_SphereMultiplier", curvePlayer.Evaluate(0));
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

        projector.DissolveDuration = projector.DissolveDuration / baseSpeed;
        trailP.colorOverLifetime = gradient;

        vCam1.SetActive(true);
        vCam2.SetActive(false);

        objectToTeleport.transform.position = posInitial.position;
        teleported = false;

        hasFinised = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasFinised)
            t += Time.deltaTime / (9.25f + effect2Delay) * baseSpeed;

        foreach (ParticleSystem p in allParticles)
        {
            mainP = p.main;
            mainP.simulationSpeed = baseSpeed * (curveSpeed.Evaluate(t));
        }

        if (effect1Active && fireRing.isStopped && !hasFinised)
            disolveObject.SetActive(true);

        if (!teleported && effect1Active && effect2Active)
            if (!IsEffect1Playing)
            {
                vCam1.SetActive(false);
                vCam2.SetActive(true);
                objectToTeleport.transform.position = posFinish.position;
                objectToTeleport.transform.rotation = posFinish.rotation;
                teleported = true;
            }

        if (effect2Active)
            if (!IsEffect2Playing && !hasFinised)
            {
                hasFinised = true;
                OnEffectFinished?.Invoke();
                anim.SetTrigger("Idle");
                
            }

        if (effect1Active && !effect2Active)
            if (!IsEffect1Playing && !hasFinised)
            {
                hasFinised = true;
                OnEffectFinished?.Invoke();
                anim.SetTrigger("Idle");
            }

        // if (IsEffect2Playing && !hasFinised)
        flashImage.color = new Color(flashImage.color.r, flashImage.color.g, flashImage.color.b, flashCurve.Evaluate(t));

        if (!hasFinised)
            foreach (var shaker in shakeControllers)
                shaker.ShakeAmplitude = intensityCurve.Evaluate(t);

        float y = minBloomEffect.Evaluate(t) * bloomMultiplier;
        float y2 = intensityCurve.Evaluate(t) * bloomMultiplier;

        bloom.intensity = Mathf.Max(y, y2);

        playerMaterial.SetFloat("_SphereMultiplier", curvePlayer.Evaluate(t));
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
        {
            t = effect2Delay / (9.25f + effect2Delay);
            vCam1.SetActive(false);
            vCam2.SetActive(true);

            objectToTeleport.transform.position = posFinish.position;
            objectToTeleport.transform.rotation = posFinish.rotation;

            brain.m_CustomBlends = onlyEffect2Blend;

            for (int i = 0; i < lenght; i++)
            {
                mainP = effect2Particles[i].main;
                mainP.startDelay = delays[i];
            }
        }
        else
        {
            t = 0;

            objectToTeleport.transform.rotation = posInitial.rotation;
            objectToTeleport.transform.position = posInitial.position;

            brain.m_CustomBlends = effect1and2Blend;
        }

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
