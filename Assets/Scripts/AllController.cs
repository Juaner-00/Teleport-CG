using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllController : MonoBehaviour
{
    [SerializeField]
    float speed;

    ParticleSystem[] ps;
    ParticleSystem.MainModule mainP;
    [SerializeField]
    GameObject disolver;
    [SerializeField]
    ParticleSystem fireRing;
    [SerializeField]
    Gradient gradient;

    ProjectorRotation projectorR;


    // Start is called before the first frame update
    void Start()
    {
        projectorR = GetComponentInChildren<ProjectorRotation>();
        ps = GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem p in ps)
        {
            mainP = p.main;
            mainP.simulationSpeed = speed;
        }
        projectorR.dissolveDuration = projectorR.dissolveDuration / speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRing.isStopped)
        {
            disolver.SetActive(true);
        }
    }
}
