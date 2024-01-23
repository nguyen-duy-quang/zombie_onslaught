using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCloseUI : MonoBehaviour
{
    public Button btnCloseUI;
    public GameObject UI;

    private void Start()
    {
        btnCloseUI.onClick.AddListener(CloseUI);
    }

    private void CloseUI()
    {
        UI.SetActive(false);

        AudioManager._instance.ButtonClickSound();
    }
}
