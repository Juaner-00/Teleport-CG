using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllController : MonoBehaviour
{

    float speed;
    [SerializeField]
    float baseSpeed, baseSize;
    float t, duracion;
    ParticleSystem[] ps;
    ParticleSystem.MainModule mainP;
    ParticleSystem.ColorOverLifetimeModule colorP;
    ParticleSystem.ShapeModule shapeP;
    ParticleSystem.TrailModule trailP;
    ParticleSystem.EmissionModule emissionP;
    ParticleSystem.VelocityOverLifetimeModule velP;
    [SerializeField]
    GameObject disolver;
    [SerializeField]
    ParticleSystem fireRing, trailParticle;
    [SerializeField]
    Gradient gradient, gradient2;
    [SerializeField]
    AnimationCurve curveSpeed;
    [SerializeField]
    GameObject[] gObject;

    int click = 0;
    ProjectorRotation projectorR;


    // Start is called before the first frame update
    void Start()
    {
        projectorR = GetComponentInChildren<ProjectorRotation>();
        ps = GetComponentsInChildren<ParticleSystem>();
        trailP = trailParticle.trails;
        foreach (GameObject gO in gObject)
        {
            gO.transform.localScale = new Vector3(gO.transform.localScale.x * baseSize, gO.transform.localScale.y, gO.transform.localScale.z * baseSize);
        }
        foreach (ParticleSystem p in ps)
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
        //projectorR.dissolveDuration = projectorR.dissolveDuration / speed;
        trailP.colorOverLifetime = gradient;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(mainP.simulationSpeed);
        if (click == 1)
        {
            t += Time.deltaTime / 18;
        }
        foreach (ParticleSystem p in ps)
        {
            mainP = p.main;
            mainP.simulationSpeed = baseSpeed * (curveSpeed.Evaluate(t));
        }

        if (Input.GetMouseButtonDown(0))
        {
            click = 1;
            foreach (ParticleSystem p in ps)
            {
                p.Play();
            }

        }
        if (fireRing.isStopped && click == 1)
        {
            disolver.SetActive(true);
            click = 0;
        }
    }
    void ClickZero()
    {
        click = 0;
    }
}
