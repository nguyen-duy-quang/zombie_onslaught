using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSetting : MonoBehaviour
{
    public Toggle low;
    public Toggle medium;
    public Toggle high;
    public Toggle ultra;

    public HandleGraphicsSetting handleGraphicsSettings;

    private void Start()
    {
        // Load giá trị cài đặt đồ họa từ PlayerPrefs
        LoadGraphicSetting();

        // Thêm các nghe sự kiện cho các Toggle
        low.onValueChanged.AddListener(OnToggleValueLow);
        medium.onValueChanged.AddListener(OnToggleValueMedium);
        high.onValueChanged.AddListener(OnToggleValueHigh);
        ultra.onValueChanged.AddListener(OnToggleValueUltra);
    }

    private void OnToggleValueLow(bool isOn)
    {
        handleGraphicsSettings.HandleGraphics(0, low, medium, high, ultra);
    }

    private void OnToggleValueMedium(bool isOn)
    {
        handleGraphicsSettings.HandleGraphics(2, medium, low, high, ultra);
    }

    private void OnToggleValueHigh(bool isOn)
    {
        handleGraphicsSettings.HandleGraphics(3, high, low, medium, ultra);
    }

    private void OnToggleValueUltra(bool isOn)
    {
        handleGraphicsSettings.HandleGraphics(5, ultra, low, medium, high);
    }

    public void LoadGraphicSetting()
    {
        handleGraphicsSettings.LoadToggleValueQuality(low, medium, high, ultra);
    }    
}
