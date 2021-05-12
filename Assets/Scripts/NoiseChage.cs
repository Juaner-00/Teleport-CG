using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseChage : MonoBehaviour
{
    ParticleSystem particle;
    ParticleSystem.NoiseModule pNoise;

    [SerializeField]
    float initialS, finalS;
    float t,t2,delay = 10;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        pNoise = particle.noise;


    }

    // Update is called once per frame
    void Update()
    {
        t2 += Time.deltaTime/10;

        delay -= t2;
        if(delay <= 0)
        {
            t += Time.deltaTime/20;
            pNoise.strength = Mathf.Lerp(initialS, finalS, t);
        }

    }
}
