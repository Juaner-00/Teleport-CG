using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] Toggle effect1Toggle;
    [SerializeField] Toggle effect2Toggle;
    [SerializeField] Slider sizeSlider;
    [SerializeField] Slider speedSlider;
    [SerializeField] TMP_Dropdown colorDropdown;
    [SerializeField] Animator anim;

    GameObject panel;

    public static Action<bool, bool, float, float, int> OnStartEffect;

    private void Awake()
    {
        panel = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        OpenUI();
    }

    private void OnEnable()
    {
        AllController.OnEffectFinished += OpenUI;
    }

    private void OnDisable()
    {
        AllController.OnEffectFinished -= OpenUI;
    }

    void OpenUI()
    {
        panel.SetActive(true);

    }

    void CloseUI()
    {
        panel.SetActive(false);
    }

    public void StartEffect()
    {
        CloseUI();

        // AllController.Instance.Activate(effect1Toggle.isOn, effect2Toggle.isOn, sizeSlider.value, speedSlider.value, colorDropdown.value);
        OnStartEffect?.Invoke(effect1Toggle.isOn, effect2Toggle.isOn, sizeSlider.value, speedSlider.value, colorDropdown.value);
        anim.SetTrigger("Cast");
    }

}
