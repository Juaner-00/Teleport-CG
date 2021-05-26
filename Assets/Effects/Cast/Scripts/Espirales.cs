using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espirales : MonoBehaviour
{
    [SerializeField] float r0;
    [SerializeField] float w0, velY0;
    [SerializeField] ParticleSystem ps;
    float t = 0;

    float r1, w1, velY1;


    private void OnEnable()
    {
        UIController.OnStartEffect += Restart;
    }

    private void OnDisable()
    {
        UIController.OnStartEffect -= Restart;
    }

    private void Start()
    {
        r1 = r0;
        w1 = w0;
        velY1 = velY0;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.isPlaying)
        {
            w1 += Time.deltaTime;
            velY1 += Time.deltaTime;

            float r = r1;
            float y = Mathf.Sin(velY1 * t);
            t += Time.deltaTime;

            float x = r * Mathf.Cos(w1 * t);
            float z = r * Mathf.Sin(w1 * t);
            Vector3 pos = new Vector3(x, y + 1, z);
            transform.localPosition = pos;
        }
    }

    void Restart(bool is1On, bool is2On, float a, float b, int c)
    {
        if (is1On)
        {
            r1 = r0;
            w1 = w0;
            velY1 = velY0;
            t = 0;
        }
    }

}
