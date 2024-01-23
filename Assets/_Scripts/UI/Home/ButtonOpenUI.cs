using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOpenUI : MonoBehaviour
{
    public Button btnOpenUI;
    public GameObject UI;

    private void Start()
    {
        btnOpenUI.onClick.AddListener(OpenUI);
    }

    private void OpenUI()
    {
        UI.SetActive(true);
        AudioManager._instance.ButtonClickSound();
    }    
}
