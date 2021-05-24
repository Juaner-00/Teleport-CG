using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorRotation : MonoBehaviour
{

    float rotSpeed = 1f;
    [SerializeField] private AnimationCurve rotationSpeed;
    private float _time;
    public float dissolveDuration;
    [SerializeField]
    float aparition;

    //[SerializeField]
    [Range(0, 1)] private float slider;
    [SerializeField]
    Color color1;

    private Renderer mat;
    // Start is called before the first frame update
    void Start()
    {
        print("color1.a");

        mat = GetComponent<Renderer>();
        color1 = mat.material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        transform.Rotate(0, rotationSpeed.Evaluate(rotSpeed), 0);
        color1.a = Mathf.Lerp(0, 1, _time / aparition < 1 ? _time / aparition : 1);
        mat.material.SetFloat("_Level", slider + _time / dissolveDuration);
        mat.material.SetColor("_Color", color1);


    }
}
