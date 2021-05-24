using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorRotation : MonoBehaviour
{
    [SerializeField] private AnimationCurve rotationSpeed;
    [SerializeField] Color color1;
    [SerializeField] float aparition;
    [SerializeField] float dissolveDuration;

    private float _time;
    float rotSpeed = 1f;

    //[SerializeField]
    [Range(0, 1)] private float slider;

    private Renderer mat;

    public float DissolveDuration { get => dissolveDuration; set => dissolveDuration = value; }

    void Start()
    {
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
