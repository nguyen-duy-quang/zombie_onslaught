using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class HandleKillMonster1 : MonoBehaviour
{
    [Header("ScriptableObjectKillZombies")]
    public ScriptableObjectKillZombies killZombie;

    public string color = "blue";

    public Messages messages;
    public UINumberKills uINumberKills;

    public UINumberOfTargetsToDestroy uINumberOfTargetsToDestroy;

    private void Start()
    {
        messages.LoadText(color, killZombie.nameZombie, messages.textKillMonster);
    }

    private void OnEnable()
    {
        // Đăng ký lắng nghe sự kiện khi script được kích hoạt
        Monster1.OnMonsterDeath += HandleMonsterDeath;
    }

    private void OnDisable()
    {
        // Hủy đăng ký lắng nghe khi script bị vô hiệu hóa hoặc bị hủy
        Monster1.OnMonsterDeath -= HandleMonsterDeath;
    }

    private void HandleMonsterDeath()
    {
        // Xử lý khi một zombie chết
        messages.KillCreatureMonster(color, killZombie.nameZombie);
        uINumberKills.DisplayNumberKills();

        uINumberOfTargetsToDestroy.CalculateNumberOfTargetsToDestroy();
    }
}
