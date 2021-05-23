using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllController : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    ParticleSystem[] ps;
    ParticleSystem.MainModule mainP;

    // Start is called before the first frame update
    void Start()
    {
        foreach(ParticleSystem p in ps)
        {
            mainP = p.main;
        }
    }

    // Update is called once per frame
    void Update()
    {       
        mainP.simulationSpeed = speed;
    }
}
