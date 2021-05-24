using System.Collections;
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

    GameObject panel;


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
        AllController.Instance.Activate(effect1Toggle.isOn, effect2Toggle.isOn, sizeSlider.value, speedSlider.value, colorDropdown.value);
    }

}
