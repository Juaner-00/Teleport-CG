using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float t = 0;

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        
    }
}
