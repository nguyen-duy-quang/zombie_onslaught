using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleKillZombie2 : MonoBehaviour
{
    [Header("ScriptableObjectKillZombies")]
    public ScriptableObjectKillZombies killZombie;

    public string color = "yellow";

    public Messages messages;
    public UINumberKills uINumberKills;

    public UINumberOfTargetsToDestroy uINumberOfTargetsToDestroy;

    private void Start()
    {
        messages.LoadText(color, killZombie.nameZombie, messages.textKillZombie2);
    }

    private void OnEnable()
    {
        // Đăng ký lắng nghe sự kiện khi script được kích hoạt
        Zombie2.OnZombieDeath += HandleZombieDeath;
    }

    private void OnDisable()
    {
        // Hủy đăng ký lắng nghe khi script bị vô hiệu hóa hoặc bị hủy
        Zombie2.OnZombieDeath -= HandleZombieDeath;
    }

    private void HandleZombieDeath()
    {
        // Xử lý khi một zombie chết
        messages.KillCreatureZombie2(color, killZombie.nameZombie);
        uINumberKills.DisplayNumberKills();

        uINumberOfTargetsToDestroy.CalculateNumberOfTargetsToDestroy();
    }
}
