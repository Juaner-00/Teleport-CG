using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase2Controller : MonoBehaviour
{
    [SerializeField] ParticleSystem system;
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

    private void Start()
    {
        main = system.main;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (!system.isPlaying && Input.GetButton("Fire1"))
        {
            system.Play();
            t = 0;
        }

        image.color = new Color(inicialColor.r, inicialColor.g, inicialColor.b, alphaCurve.Evaluate(t));
        t += Time.deltaTime / duration;
        t = Mathf.Clamp(t, 0, duration);
    }
}
