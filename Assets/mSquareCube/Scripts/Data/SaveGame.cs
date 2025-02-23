using System;
using UnityEngine;
using Zenject;

public enum Language
{
    rus,
    eng,
}

[Serializable]
public class SaveGame : IDisposable, IInitializable
{
    public GameData Data { get; set; }

    public const int startLevelIndex = 2;

    private const string KEY_SAVE = "SAVE";

    public void Initialize()
    {
        Debug.Log("Save Init");
        LoadData();
    }

    public void Dispose()
    {
        Debug.Log("Dispose Save");
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(KEY_SAVE))
        {
            string jsonString = PlayerPrefs.GetString(KEY_SAVE);
            Data = JsonUtility.FromJson<GameData>(jsonString);
        }
        else
        {
            Data = new GameData();
        }
    }

    public void ClearGameProgress()
    {
        Data.LevelSingleIndex = startLevelIndex;
        SaveData();
    }

    public void SaveData() 
    {
        string jsonSave = JsonUtility.ToJson(Data);
        PlayerPrefs.SetString(KEY_SAVE, jsonSave);
        PlayerPrefs.Save();
    }
}

[Serializable]
public class GameData
{
    public Language CurrentLanguage;
    public int LevelSingleIndex;
    public int LevelJointsIndex;
    public SettingsGame Settings;

    public GameData()
    {
        CurrentLanguage = Language.eng;
        LevelSingleIndex = SaveGame.startLevelIndex;
        LevelJointsIndex = 0;
        Settings = new SettingsGame();
    }
}

[Serializable]
public class SettingsGame
{
    public bool PostProcessing;
    public bool VSing;
    public float VolumeMusic;
    public float VolumeSound;

    public SettingsGame()
    {
        PostProcessing = true;
        VSing = false;
        VolumeMusic = 0;
        VolumeSound = 0;
    }
}
