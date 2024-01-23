using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class TextEffect : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        // Đảm bảo bạn gán TextMeshPro vào trường textMeshPro trong Inspector.
        if (textMeshPro == null)
            textMeshPro = GetComponent<TextMeshProUGUI>();

        // Ẩn TextMeshPro ban đầu
        textMeshPro.alpha = 0f;
    }

    public void ShowHealthText(string healthIsAdded)
    {
        textMeshPro.text = "Health increased by " + healthIsAdded + " points using the healing potion!";
        ShowText();
    }    

    private void ShowText()
    {
        // Hiệu ứng xuất hiện trong 1 giây (mờ dần)
        textMeshPro.DOFade(1f, 1f).OnComplete(() =>
        {
            // Hiệu ứng biến mất sau 2 giây (nét đến mờ dần)
            textMeshPro.DOFade(0f, 1f).SetDelay(2f);
        });
    }
}
