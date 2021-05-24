using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase2Controller : MonoBehaviour
{
    [SerializeField] ParticleSystem mainSystem;
    [SerializeField] ParticleSystem dust;
    [SerializeField] ParticleSystem dustParticles;
    [SerializeField] ParticleSystem dustAfter;
    [SerializeField] ParticleSystem debris;
    [SerializeField] Image image;
    [SerializeField] AnimationCurve alphaCurve;
    [SerializeField] float duration;

    float t = 0;

    Color inicialColor;

    ParticleSystem.MainModule main;

    ParticleSystem[] systems;

    public static Action OnPhase2Finised;

    bool hasFinised;


    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        Instance = this;

        systems = mainSystem.GetComponentsInChildren<ParticleSystem>();
    }

    private void Start()
    {
        main = mainSystem.main;
        main.startLifetime = duration;

        main = dust.main;
        main.duration = 0.7f * duration;

        main = dustParticles.main;
        main.duration = 0.4f * duration;

        main = dustAfter.main;
        main.startDelay = 0.7f * duration;
        main.duration = 0.6f * duration;

        main = debris.main;
        main.startDelay = 0.9f * duration;

        inicialColor = image.color;

        hasFinised = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsPlaying && !hasFinised)
        {
            hasFinised = true;
            OnPhase2Finised?.Invoke();
        }

        if (IsPlaying)
        {
            image.color = new Color(inicialColor.r, inicialColor.g, inicialColor.b, alphaCurve.Evaluate(t));
            t += Time.deltaTime / duration;
            t = Mathf.Clamp(t, 0, duration);
        }
    }

    public void Activate()
    {
        mainSystem.Play();
        t = 0;
        hasFinised = false;
    }


    bool IsPlaying
    {
        get
        {
            foreach (ParticleSystem p in systems)
            {
                if (p.isPlaying)
                    return true;
            }
            return false;
        }
    }

    public static Phase2Controller Instance { get; private set; }
}
