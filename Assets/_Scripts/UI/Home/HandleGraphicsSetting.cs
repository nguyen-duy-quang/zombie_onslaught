using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleGraphicsSetting : MonoBehaviour
{
    private int indexQuality;

    public GraphicsSetting graphicsSetting;

    private void Start()
    {
        graphicsSetting.LoadGraphicSetting();
    }

    public void HandleGraphics(int index, Toggle main, Toggle secondary1, Toggle secondary2, Toggle secondary3)
    {
        indexQuality = index;
        secondary1.isOn = false;
        secondary2.isOn = false;
        secondary3.isOn = false;

        if (secondary1.isOn == false && secondary2.isOn == false && secondary3.isOn == false)
        {
            main.isOn = true;
            QualitySettings.SetQualityLevel(indexQuality);

            Datas data = new Datas();
            // Lưu giá trị cài đặt đồ họa vào PlayerPrefs
            PlayerPrefs.SetInt(data.graphicsQuality, indexQuality);
            PlayerPrefs.Save();
        }
    }

    public void LoadToggleValueQuality(Toggle secondary1, Toggle secondary2, Toggle secondary3, Toggle secondary4)
    {
        Datas data = new Datas();

        // Load giá trị cài đặt đồ họa từ PlayerPrefs
        int savedGraphicsQuality = PlayerPrefs.GetInt(data.graphicsQuality, 0);

        // Thiết lập chất lượng đồ họa theo giá trị đã lưu
        QualitySettings.SetQualityLevel(savedGraphicsQuality);

        // Thiết lập trạng thái của Toggle dựa trên giá trị cài đặt đồ họa đã lưu
        if (savedGraphicsQuality == 0)
        {
            secondary1.isOn = true;
            secondary2.isOn = false;
            secondary3.isOn = false;
            secondary4.isOn = false;
        }
        else if (savedGraphicsQuality == 2)
        {
            secondary1.isOn = false;
            secondary2.isOn = true;
            secondary3.isOn = false;
            secondary4.isOn = false;
        }
        else if (savedGraphicsQuality == 3)
        {
            secondary1.isOn = false;
            secondary2.isOn = false;
            secondary3.isOn = true;
            secondary4.isOn = false;
        }
        else if (savedGraphicsQuality == 5)
        {
            secondary1.isOn = false;
            secondary2.isOn = false;
            secondary3.isOn = false;
            secondary4.isOn = true;
        }
    }
}
