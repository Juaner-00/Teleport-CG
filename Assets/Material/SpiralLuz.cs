using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralLuz : MonoBehaviour
{
    public ParticleSystem spiral;
    public float radioI;
    public float angularV;
    public float velocidadY;
    float r,y,tiempoY=0,tiempoXZ, tiempoChangeVel;
    float fromInicial = 0, toInicial;
    int state;
    float rnd;

    public float moreVel;
    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        r = radioI;

    }
    // Update is called once per frame
    void Update()
    {

        if (state == 0)
        {
            rnd = Random.Range(1, 9);
            toInicial = rnd;
            state = 1;
            tiempoY = 0;
        }
        if (state == 1)
        {
            
            
            tiempoY += Time.deltaTime/4;
            tiempoY = Mathf.Clamp(tiempoY, 0f, 1f);
            y = Mathf.Lerp(fromInicial, toInicial, tiempoY);
            if (y == toInicial)
            {
                fromInicial = toInicial;
                state = 0;
                tiempoY = 0;
            }
        }
        tiempoXZ += Time.deltaTime;

        tiempoChangeVel += Time.deltaTime / 5;
        moreVel = Mathf.Lerp(0, angularV, tiempoChangeVel);
        spiral.transform.localPosition = new Vector3(r * Mathf.Cos(moreVel * tiempoXZ), r*  Mathf.Cos(y), r * Mathf.Sin(moreVel * tiempoXZ));
    }
}