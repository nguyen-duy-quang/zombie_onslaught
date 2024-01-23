using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveCurrency : MonoBehaviour
{
    private string savePath;

    public ScriptableObjectCurrency money;
    public ScriptableObjectCurrency gem;

    private void Awake()
    {
        savePath = Application.persistentDataPath + "/save.dat";
    }

    public void SaveData()
    {
        // Tạo một đối tượng lưu trữ dữ liệu cần lưu
        CurrencyData currencyData = new CurrencyData();
        currencyData.money = money.currency;
        currencyData.gem = gem.currency;

        // Sử dụng BinaryFormatter để ghi dữ liệu vào FileStream
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(savePath);
        binaryFormatter.Serialize(fileStream, currencyData);
        fileStream.Close();

        Debug.Log("Data saved successfully.");
    }

    public void LoadData()
    {
        if (File.Exists(savePath))
        {
            // Sử dụng BinaryFormatter để đọc dữ liệu từ FileStream
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(savePath, FileMode.Open);
            CurrencyData currencyData = (CurrencyData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            // Gán dữ liệu từ saveData vào ScriptableObject
            money.currency = currencyData.money;
            gem.currency = currencyData.gem;

            Debug.Log("Data loaded successfully.");
        }
        else
        {
            Debug.LogWarning("No saved data found.");
        }
    }
}
