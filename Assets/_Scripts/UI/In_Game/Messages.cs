using System.Collections;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Messages : MonoBehaviour
{
    [Header("GameObject TextMeshPro in Message")]
    public GameObject objectTextKillZombie1; // Prefab của TextMeshPro
    public GameObject objectTextKillZombie2; // Prefab của TextMeshPro
    public GameObject objectTextKillZombie3; // Prefab của TextMeshPro
    public GameObject objectTextKillMonster1; // Prefab của TextMeshPro

    [Header("TextMesPro in Message")]
    public TextMeshProUGUI textKillZombie1;
    public TextMeshProUGUI textKillZombie2;
    public TextMeshProUGUI textKillZombie3;
    public TextMeshProUGUI textKillMonster;

    [Header("String Content")]
    public string textContent;

    [Header("Content in Scroll view")]
    public Transform content; // Đối tượng Content trong ScrollView

    public int maxItems = 4; // Số lượng tối đa của item trong content

    public void KillCreatureZombie1(string color, string creatureName)
    {
        KillCreature(color, creatureName, textKillZombie1, objectTextKillZombie1);
    }
    public void KillCreatureZombie2(string color, string creatureName)
    {
        KillCreature(color, creatureName, textKillZombie2, objectTextKillZombie2);
    }
    public void KillCreatureZombie3(string color, string creatureName)
    {
        KillCreature(color, creatureName, textKillZombie3, objectTextKillZombie3);
    }

    public void KillCreatureMonster(string color, string creatureName)
    {
        KillCreature(color, creatureName, textKillMonster, objectTextKillMonster1);
    }

    public void KillCreature(string color, string creatureName, TextMeshProUGUI textComponent, GameObject prefab)
    {
        // Kiểm tra số lượng item hiện tại trong content
        if (content.childCount >= maxItems)
        {
            // Xóa đi item đầu tiên (item cũ nhất)
            Destroy(content.GetChild(0).gameObject);
        }

        // Tạo ra một bản sao mới của Prefab
        GameObject newText = Instantiate(prefab, content);

        // Có thể thêm các cấu hình khác cho TextMeshPro ở đây nếu cần
        //textComponent.text = $"<size=25>{textContent} [<color={color}>{creatureName}</color>]</size>";
        LoadText(color, creatureName, textComponent);

        // Đặt vị trí và xoay vật thể theo ý muốn
        newText.transform.position = new Vector3(0, 0, 0);
        newText.transform.rotation = Quaternion.identity;
    }

    public void LoadText(string color, string creatureName, TextMeshProUGUI textComponent)
    {
        textComponent.text = $"<size=25>{textContent} [<color={color}>{creatureName}</color>]</size>";
    }    
}