using System;
using System.Collections;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeSliderController : MonoBehaviour
{
    public ScriptableObjectRule rule;

    public Slider timeSlider;
    public TMP_Text timeText;
    public float totalTimeInSeconds = 300f; // Thời gian ban đầu, ví dụ 5 phút

    private float elapsedTime = 0f;
    private float pausedTime = 0f;
    private bool isPaused = false;

    [Header("GameOver")]
    public HandleGameOver handleGameOver;

    public UINumberOfTargetsToDestroy uINumberOfTargetsToDestroy;

    void Start()
    {
        totalTimeInSeconds = rule.time;
        StartCoroutine(DecreaseSliderAndDisplayTime());
    }

    IEnumerator DecreaseSliderAndDisplayTime()
    {
        while (elapsedTime < totalTimeInSeconds)
        {
            if (!isPaused)
            {
                // Tính toán giá trị mới của slider dựa trên thời gian đã trôi qua
                float normalizedTime = elapsedTime / totalTimeInSeconds;
                timeSlider.value = Mathf.Lerp(1f, 0f, normalizedTime);

                // Cập nhật giá trị TextMeshPro
                TimeSpan timeSpan = TimeSpan.FromSeconds(totalTimeInSeconds - elapsedTime);
                timeText.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);

                // Cập nhật thời gian đã trôi qua
                elapsedTime += Time.deltaTime;
            }

            yield return null;
        }

        // Đảm bảo giá trị cuối cùng của slider là 0
        timeSlider.value = 0f;
        timeText.text = "00:00";

        // Khi chạy xong và nếu chưa tiêu diệt hết mục tiêu
        if (elapsedTime >= totalTimeInSeconds && uINumberOfTargetsToDestroy.currentNumberOfTargetsToDestroy <= 0)
        {
            RunComplete();
        }
        else
        {
            DeathMenu();
        }    
    }

    private void RunComplete()
    {
        handleGameOver.VictoryGame();
    }

    private void DeathMenu()
    {
        handleGameOver.DefeatGame();
    }

    public void StopTime()
    {
        if (isPaused)
        {
            // Nếu đang tạm dừng, tính thời gian đã tạm dừng và cộng vào elapsedTime
            elapsedTime += Time.time - pausedTime;
        }
        else
        {
            // Nếu không tạm dừng, lưu thời điểm khi bắt đầu tạm dừng
            pausedTime = Time.time;
        }

        // Chuyển đổi trạng thái tạm dừng
        isPaused = !isPaused;
    }    
}
