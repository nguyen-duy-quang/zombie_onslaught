using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveWeaponLevel : MonoBehaviour
{
    private string savePath;

    public ScriptableObjectWeaponUpgrades rifeUpgrade;
    public ScriptableObjectWeaponUpgrades pistolUpgrade;

    private void Awake()
    {
        savePath = Application.persistentDataPath + "/saveWeaponLevel.dat";
        Debug.Log(savePath);
    }

    public void SaveData()
    {
        // Tạo một đối tượng lưu trữ dữ liệu cần lưu
        LevelData levelData = new LevelData();
        levelData.rifelDamage = rifeUpgrade.damage;
        levelData.rifleLevelValue = rifeUpgrade.levelValue;
        levelData.rifleLevel = rifeUpgrade.weaponLevel;

        levelData.pistolDamage = pistolUpgrade.damage;
        levelData.pistolLevelValue = pistolUpgrade.levelValue;
        levelData.pistolLevel = pistolUpgrade.weaponLevel;

        // Sử dụng BinaryFormatter để ghi dữ liệu vào FileStream
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(savePath);
        binaryFormatter.Serialize(fileStream, levelData);
        fileStream.Close();

        Debug.Log("Saved Level successfully.");
    }

    public void LoadData()
    {
        if (File.Exists(savePath))
        {
            // Sử dụng BinaryFormatter để đọc dữ liệu từ FileStream
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(savePath, FileMode.Open);
            LevelData levelData = (LevelData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            // Gán dữ liệu
            rifeUpgrade.damage = levelData.rifelDamage;
            rifeUpgrade.levelValue = levelData.rifleLevelValue;
            rifeUpgrade.weaponLevel = levelData.rifleLevel;

            pistolUpgrade.damage = levelData.pistolDamage;
            pistolUpgrade.levelValue = levelData.pistolLevelValue;
            pistolUpgrade.weaponLevel = levelData.pistolLevel;

            Debug.Log("Loaded Level successfully.");
        }
        else
        {
            Debug.LogWarning("No saved data found.");
        }
    }
}
