using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveAudio : MonoBehaviour
{
    private string savePath;

    public ScriptableObjectAudio music;
    public ScriptableObjectAudio FX;

    private void Awake()
    {
        savePath = Application.persistentDataPath + "/audio.dat";
    }

    private void Start()
    {
        LoadData();
    }

    public void SaveData()
    {
        // Tạo một đối tượng lưu trữ dữ liệu cần lưu
        AudioData audioData = new AudioData();
        audioData.musicValue = music.audioVolume;
        audioData.FXValue = FX.audioVolume;

        // Sử dụng BinaryFormatter để ghi dữ liệu vào FileStream
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(savePath);
        binaryFormatter.Serialize(fileStream, audioData);
        fileStream.Close();

        Debug.Log("Saved Audio successfully.");
    }

    public void LoadData()
    {
        if (File.Exists(savePath))
        {
            // Sử dụng BinaryFormatter để đọc dữ liệu từ FileStream
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(savePath, FileMode.Open);
            AudioData audioData = (AudioData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            // Gán dữ liệu
            music.audioVolume = audioData.musicValue;
            FX.audioVolume = audioData.FXValue;
            Debug.Log("Loaded Audio successfully.");
        }
        else
        {
            Debug.LogWarning("No saved Audio data found.");
        }
    }

    /*private void SaveData(float value, string debugMessage)
    {
        // Tạo một đối tượng lưu trữ dữ liệu cần lưu
        AudioData audioData = new AudioData();
        audioData.musicValue = value;

        // Sử dụng BinaryFormatter để ghi dữ liệu vào FileStream
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(savePath);
        binaryFormatter.Serialize(fileStream, audioData);
        fileStream.Close();

        Debug.Log(debugMessage);
    }

    private void LoadData(ref float value, string debugMessage)
    {
        if (File.Exists(savePath))
        {
            // Sử dụng BinaryFormatter để đọc dữ liệu từ FileStream
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(savePath, FileMode.Open);
            AudioData audioData = (AudioData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            // Gán dữ liệu từ saveData vào ScriptableObject
            value = audioData.musicValue;

            Debug.Log(debugMessage);
        }
        else
        {
            Debug.LogWarning("No saved data found.");
        }
    }

    public void SaveMusicVolume(ScriptableObjectAudio audio)
    {
        SaveData(audio.audioVolume, "Music Data saved successfully.");
    }

    public void LoadMusicVolume(ScriptableObjectAudio audio)
    {
        LoadData(ref audio.audioVolume, "Music Data loaded successfully.");
    }

    public void SaveFXVolume(ScriptableObjectAudio audio)
    {
        SaveData(audio.audioVolume, "FX Data saved successfully.");
    }

    public void LoadFXVolume(ScriptableObjectAudio audio)
    {
        LoadData(ref audio.audioVolume, "FX Data loaded successfully.");
    }*/
}
