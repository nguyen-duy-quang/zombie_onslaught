using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectMap : MonoBehaviour
{
    public ScriptableObjectRule rule;
    public MapImage mapImage;

    public TextMeshProUGUI textIntroduction;
    public TextMeshProUGUI textNumberOfKill;
    public TextMeshProUGUI textTime;
    public Image map;

    private int time;

    private void Start()
    {
        time = rule.time;
        //DisplayRule();
    }

    // Xử lý load nội dung text từ ScriptableObjectRule
    public void DisplayRule()
    {
        textIntroduction.text = rule.introduction;
        textNumberOfKill.text = "Target Annihilation: " + rule.numberOfKill.ToString();
        TimerDisplay();
        map.sprite = mapImage.mapImage.mapIcon;
    }

    private void TimerDisplay()
    {
        // Chuyển đổi thời gian thành định dạng mm:ss
        string minutes = Mathf.Floor(time / 60).ToString("00");
        string seconds = (time % 60).ToString("00");

        // Hiển thị thời gian trên TextMeshPro
        textTime.text = "Time: " + minutes + ":" + seconds;
    }
}
