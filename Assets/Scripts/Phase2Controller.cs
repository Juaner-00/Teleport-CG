using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase2Controller : MonoBehaviour
{
    [SerializeField] ParticleSystem system;
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
