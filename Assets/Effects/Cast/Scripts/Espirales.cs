using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espirales : MonoBehaviour
{
    [SerializeField] float r0;
    [SerializeField] float w, velY;
    [SerializeField] ParticleSystem ps;
    float t = 0;

    // Update is called once per frame
    void Update()
    {
        if (ps.isPlaying)
        {
            w += Time.deltaTime;
            velY += Time.deltaTime;

            float r = r0;
            float y = Mathf.Sin(velY * t);
            t += Time.deltaTime;

            float x = r * Mathf.Cos(w * t);
            float z = r * Mathf.Sin(w * t);
            Vector3 pos = new Vector3(x, y + 1, z);
            transform.localPosition = pos;
        }
    }
}
