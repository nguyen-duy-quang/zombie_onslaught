using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectMap : MonoBehaviour
{
    private Button lastPressedButton;

    public Button btnSelectMap1;
    public Button btnSelectMap2;
    public Button btnSelectMap3;
    public Button btnSelectStart;

    public ButtonSelectMap selectMap1;
    public ButtonSelectMap selectMap2;
    public ButtonSelectMap selectMap3;

    public GameManager gameManager;

    public string[] mapNames;

    public Color normalColor;
    public Color newColor;

    public TextMeshProUGUI textMap1;
    public TextMeshProUGUI textMap2;
    public TextMeshProUGUI textMap3;

    void Start()
    {
        // Gắn sự kiện cho các button
        btnSelectMap1.onClick.AddListener(() => OnButtonPressed(btnSelectMap1));
        btnSelectMap2.onClick.AddListener(() => OnButtonPressed(btnSelectMap2));
        btnSelectMap3.onClick.AddListener(() => OnButtonPressed(btnSelectMap3));
        btnSelectStart.onClick.AddListener(OnStartButtonPressed);
    }

    void OnButtonPressed(Button button)
    {
        lastPressedButton = button;

        // Lưu trữ thông tin về button được chọn
        if (button == btnSelectMap1)
        {
            textMap1.color = newColor;
            textMap2.color = normalColor;
            textMap3.color = normalColor;

            selectMap1.DisplayRule();
        }
        else if (button == btnSelectMap2)
        {
            textMap1.color = normalColor;
            textMap2.color = newColor;
            textMap3.color = normalColor;

            selectMap2.DisplayRule();
        }
        else if (button == btnSelectMap3)
        {
            textMap1.color = normalColor;
            textMap2.color = normalColor;
            textMap3.color = newColor;

            selectMap3.DisplayRule();
        }
    }

    void OnStartButtonPressed()
    {
        if (lastPressedButton == btnSelectMap1)
        {
            Debug.Log("Đã kích hoạt map 1");
            gameManager.SwitchGameScene(mapNames[0]);
        }
        else if (lastPressedButton == btnSelectMap2)
        {
            gameManager.SwitchGameScene(mapNames[1]);
        }
        else if (lastPressedButton == btnSelectMap3)
        {
            Debug.Log("Đã kích hoạt map 3");
            gameManager.SwitchGameScene(mapNames[2]);
        }

        AudioManager._instance.ButtonClickSound();
    }
}
