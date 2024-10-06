using System;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using static LevelList;
using static SaveGame;

[CreateAssetMenu(fileName = "New Level List", menuName = "Level List")]
public class LevelList : ScriptableObject
{
    public enum Levels
    {
        Coop,
        Begin,
        TwoShot,
        Gravity,
        MoveCube
    }

    [SerializeField] private List<LevelsCotegory> _levelsCotegory = new List<LevelsCotegory>();
    [SerializeField] private List<LevelData> _countCoopLevels = new List<LevelData>();
    [SerializeField] private List<LevelData> _countSingleLevels = new List<LevelData>();


    public int GetCoopNextIndex(int currentIndex)
    {
        if (currentIndex < _countCoopLevels.Count)
            return _countCoopLevels[currentIndex++].buildIndex;
        else
            throw new Exception($"Ты вишел за приделы массива кооперативных уровней дурачек {currentIndex}");
    }

    public LevelsCotegory[] GetLevelsCotegories() => _levelsCotegory.Where(p => p.GetCotegory() != Levels.Coop).ToArray();

    public int GetCurrentSceneSingleLevel(int currentIndexLevel)
    {
        return _countSingleLevels[currentIndexLevel].buildIndex;
    }
    public int GetBuildIndexBySaveForJoinMode(int saveIndex)
    {
        return _countCoopLevels[saveIndex].buildIndex;
    }

    public int GetSingleNextLevel(int currentLevel)
    {
        if (currentLevel < _countSingleLevels.Count - 1)
            return _countSingleLevels[currentLevel].buildIndex;
        else
            throw new Exception("Вышел за приделы массива одиночных уровней");
    }

#if UNITY_EDITOR
    [ContextMenu("Populate From Build Settings")]
    private void PopulateFromBuildSettings()
    {
        _levelsCotegory.Clear();

        // Получаем все сцены из настроек сборки и добавляем их в список
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        string lastPath = "";
        _levelsCotegory.Add(new LevelsCotegory());
        int indexScene = 0;
        foreach (EditorBuildSettingsScene scene in scenes)
        {
            if (scene.enabled)
            {
                string scenePath = scene.path;
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                LevelData levelData = new LevelData();
                levelData.levelName = sceneName;
                levelData.buildIndex = indexScene;
                indexScene++;

                if(lastPath == "")
                {
                    lastPath = scenePath.Replace(sceneName, "");
                    Debug.Log(lastPath);
                }

                if(lastPath != "")
                {
                    if(lastPath == scenePath.Replace(sceneName, ""))
                    {
                        _levelsCotegory[_levelsCotegory.Count - 1].Levels.Add(levelData);
                    }
                    else
                    {
                        _levelsCotegory.Add(new LevelsCotegory());
                        _levelsCotegory[_levelsCotegory.Count - 1].Levels.Add(levelData);
                    }
                    lastPath = scenePath.Replace(sceneName, "");
                }
            }
        }

        // Обновляем активную копию ScriptableObject в редакторе
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }


    [ContextMenu("CountingLevels")]
    private void CountingLevels()
    {
        _countCoopLevels = _levelsCotegory.Where(p=>p.GetCotegory() == Levels.Coop).FirstOrDefault().Levels;

        var singleModeCotegory = _levelsCotegory.Where(p => p.GetCotegory() != Levels.Coop).ToArray();
        _countSingleLevels.Clear();
        foreach (var item in singleModeCotegory)
        {
            _countSingleLevels.AddRange(item.Levels);
        }
    }
#endif
}

[Serializable]
public class LevelsCotegory
{
    public List<LevelData> Levels = new List<LevelData>();
    [SerializeField] private string _nameRussian;
    [SerializeField] private string _nameEnglish;
    [SerializeField] private Levels _cotegory;
    [SerializeField] private Sprite _icon;

    public Levels GetCotegory() => _cotegory;

    public Sprite GetSprite() => _icon;

    public string GetName(Language language) => language == Language.rus ? _nameRussian : _nameEnglish;
}

[Serializable]
public class LevelData
{
    public string levelName;
    public int buildIndex;
}
