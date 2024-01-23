using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleWeaponUpgrades : MonoBehaviour
{
    public SaveWeaponLevel saveWeaponLevel;

    public void WeaponUpgrades(UICurrency uICurrency, int currentGem, ref int maxLevel, ref float currentValue, ref float value, float maxValue, ref int damage, ref int level)
    {
        if (uICurrency.scriptableObjectCurrency.currency >= currentGem)
        {
            // Gọi hàm WeaponLevel trước để tránh lặp lại code kiểm tra currency
            WeaponLevel(uICurrency, currentGem, ref maxLevel, ref currentValue, ref value, maxValue, ref damage, ref level);

            // Lưu dữ liệu và hiển thị tiền tệ sau khi nâng cấp
            uICurrency.saveCurrency.SaveData();
            uICurrency.DisplayCurrency();

            // Lưu dữ liệu vũ khí sau khi nâng cấp
            saveWeaponLevel.SaveData();
        }
        else if (uICurrency.scriptableObjectCurrency.currency <= 0)
        {
            uICurrency.scriptableObjectCurrency.currency = 0;
        }
    }

    private void WeaponLevel(UICurrency uICurrency, int currentGem, ref int maxLevel, ref float currentValue, ref float value, float maxValue, ref int damage, ref int level)
    {
        value += currentValue;

        if(level < maxLevel)
        {
            // Giảm tiền sau khi nâng cấp
            uICurrency.scriptableObjectCurrency.currency -= currentGem;
        }    

        if (value >= maxValue && level < maxLevel)
        {
            value = 0f;
            damage += 1;
            level += 1;
        }

        // Nếu level vượt quá maxLevel thì đặt lại level và giá trị hiện tại
        if (level >= maxLevel)
        {
            value = maxValue;
            level = maxLevel;
        }
    }
}
