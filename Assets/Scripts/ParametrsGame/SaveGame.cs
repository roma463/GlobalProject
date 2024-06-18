using System;
using UnityEngine;
using static SaveGame;

public class SaveGame : MonoBehaviour
{
    public static SaveGame Instance { private set; get; }
    public Data Saves { get; set; }
    [SerializeField] private LevelList _levelList;

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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Saves.LevelJointsIndex = 0;
            Saves.LevelSingleIndex = 0;
            SaveData();
        }
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
            Saves = JsonUtility.FromJson<Data>(jsonString);
            //Saves = new Data();
        }
        else
        {
            Saves = new Data();
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
public class Data
{
    public Language CurrentLanguage;
    public int LevelSingleIndex;
    public int LevelJointsIndex;
    public SettingsGame Settings;
    public Data()
    {
        CurrentLanguage = Language.rus;
        LevelSingleIndex = 0;
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
