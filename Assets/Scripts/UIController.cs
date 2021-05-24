using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown colorDropdown;

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
        Phase2Controller.OnPhase2Finised += OpenUI;
    }

    private void OnDisable()
    {
        Phase2Controller.OnPhase2Finised -= OpenUI;
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
        Phase2Controller.Instance.Activate();
    }

}
