using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorRotation : MonoBehaviour
{

    public float rotSpeed =1f;
    [SerializeField]private AnimationCurve rotationSpeed;

    [SerializeField] [Range(0, 1)] private float slider;

   
    
    private Renderer mat; 
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

    
        transform.Rotate(0, rotationSpeed.Evaluate(rotSpeed)  , 0);
        mat.material.SetFloat("_Level",slider);
}
}
