using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

[Serializable]
class PlayerData
{
    public int currentLevelIndex;
    public float[] highScores = new float[20];
}

public class SaveAndLoad : MonoBehaviour
{
    public static SaveAndLoad saveAndLoad;

    public int currentLevelIndex;               // Pidetään yllä pelaajan edistymistä scenen indeksin avulla
    public float[] highScores = new float[20];  // Array highscoreille

    void Awake()
    {
        if (saveAndLoad == null)
        {
            DontDestroyOnLoad(gameObject);
            saveAndLoad = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Load();
    }

    public void ResetStats()
    {
        currentLevelIndex = 0;

        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i] = 0f;
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();

        data.currentLevelIndex = currentLevelIndex;

        for (int i = 0; i < highScores.Length; i++)
        {
            data.highScores[i] = highScores[i];
        }

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            currentLevelIndex = data.currentLevelIndex;

            for (int i = 0; i < highScores.Length; i++)
            {
                highScores[i] = data.highScores[i];
            }

        }
    }
}
