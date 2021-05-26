using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorRotation : MonoBehaviour
{
    [SerializeField] private AnimationCurve rotationSpeed;
    [SerializeField] public Color color;
    [SerializeField] float aparition;
    [SerializeField] float dissolveDuration;

    float aparitionBase, dissolveBase;

    private float _time;
    float rotSpeed = 1f;

    //[SerializeField]
    [Range(0, 1)] private float slider;

    private Renderer mat;

    bool isActive;

    private void OnEnable()
    {
        UIController.OnStartEffect += Restart;
    }

    private void OnDisable()
    {
        UIController.OnStartEffect -= Restart;
    }

    void Start()
    {
        isActive = false;
        aparitionBase = aparition;
        dissolveBase = dissolveDuration;

        _time = 0;

        mat = GetComponent<Renderer>();
        // color1 = mat.material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            _time += Time.deltaTime;
            transform.Rotate(0, rotationSpeed.Evaluate(rotSpeed), 0);
            color.a = Mathf.Lerp(0, 1, _time / aparition < 1 ? _time / aparition : 1);
            mat.material.SetFloat("_Level", slider + _time / dissolveDuration);
            mat.material.SetColor("_Color", color);
        }
    }

    public void Activate()
    {
        if (!isActive)
            isActive = true;
    }

    public void Desactivate()
    {
        if (isActive)
        {
            aparition = aparitionBase;
            dissolveDuration = dissolveBase;
            isActive = false;
            _time = 0;
        }
    }

    void Restart(bool a, bool b, float c, float speed, int d)
    {
        aparition = aparitionBase / speed;
        dissolveDuration = dissolveBase / speed;
    }
}
