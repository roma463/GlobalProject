using System;
using System.Text;
using UnityEngine;
using static SaveGame;

public class SaveGame : MonoBehaviour
{
    public static SaveGame Instance { private set; get; }
    public Save Saves { get; set; }

    private const string KEY_SAVE = "SAVE";

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadData();
    }

    public enum Language
    {
        rus,
        eng,
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(KEY_SAVE))
        {
            string jsonString = PlayerPrefs.GetString(KEY_SAVE);
            print(Encoding.Unicode.GetByteCount(jsonString));
            Saves = JsonUtility.FromJson<Save>(jsonString);
        }
        else
        {
            Saves = new Save();
        }
    }

    public void SaveData() 
    {
        string jsonSave = JsonUtility.ToJson(Saves);
        PlayerPrefs.SetString(KEY_SAVE, jsonSave);
        PlayerPrefs.Save();
    }
}

[Serializable]
public class Save
{
    public Language CurrentLanguage;
    public int LevelIndex;
    public SettingsGame Settings;
    public Save()
    {
        CurrentLanguage = Language.rus;
        LevelIndex = 1;
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
