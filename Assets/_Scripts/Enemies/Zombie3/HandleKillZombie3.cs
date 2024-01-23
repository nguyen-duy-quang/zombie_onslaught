using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleKillZombie3 : MonoBehaviour
{
    [Header("ScriptableObjectKillZombies")]
    public ScriptableObjectKillZombies killZombie;

    public string color = "green";

    public Messages messages;
    public UINumberKills uINumberKills;

    public UINumberOfTargetsToDestroy uINumberOfTargetsToDestroy;

    private void Start()
    {
        messages.LoadText(color, killZombie.nameZombie, messages.textKillZombie3);
    }

    private void OnEnable()
    {
        // Đăng ký lắng nghe sự kiện khi script được kích hoạt
        Zombie3.OnZombieDeath += HandleZombieDeath;
    }

    private void OnDisable()
    {
        // Hủy đăng ký lắng nghe khi script bị vô hiệu hóa hoặc bị hủy
        Zombie3.OnZombieDeath -= HandleZombieDeath;
    }

    private void HandleZombieDeath()
    {
        // Xử lý khi một zombie chết
        messages.KillCreatureZombie3(color, killZombie.nameZombie);
        uINumberKills.DisplayNumberKills();

        uINumberOfTargetsToDestroy.CalculateNumberOfTargetsToDestroy();
    }
}
