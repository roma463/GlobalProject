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

#if UNITY_EDITOR
    [SerializeField] private List<LevelsCotegory> _levelsCotegory = new List<LevelsCotegory>();
#endif

    private void OnValidate()
    {
        foreach (var cotegory in _levelsCotegory)
        {
            for (int i = 0; i < cotegory.Levels.Count; i++)
            {

                if (cotegory.Levels[i]?.Scene.name != cotegory.Levels[i].StringNameLevel)
                {
                    cotegory.Levels[i].StringNameLevel = cotegory.Levels[i].Scene.name;
                }
            }
        }
    }

    //public int GetCurrentSceneSingleLevel(int currentIndexLevel)
    //{
    //    return _countSingleLevels[currentIndexLevel].buildIndex;
    //}

    //public int GetBuildIndexBySaveForJoinMode(int saveIndex)
    //{
    //    return _countCoopLevels[saveIndex].buildIndex;
    //}

    //public int GetSingleNextLevel(int currentLevel)
    //{
    //    if (currentLevel < _countSingleLevels.Count - 1)
    //        return _countSingleLevels[currentLevel].buildIndex;
    //    else
    //        throw new Exception("Вышел за приделы массива одиночных уровней");
    //}

#if UNITY_EDITOR



    [ContextMenu("Populate From Build Settings")]
    private void PopulateFromBuildSettings()
    {
        _levelsCotegory.Clear();

        // Получаем все сцены из настроек сборки и добавляем их в список
        //EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        //string lastPath = "";
        //_levelsCotegory.Add(new LevelsCotegory());
        //int indexScene = 0;
        //foreach (EditorBuildSettingsScene scene in scenes)
        //{
        //    if (scene.enabled)
        //    {
        //        string scenePath = scene.path;
        //        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        //        LevelData levelData = new LevelData();
        //        levelData.levelName = sceneName;
        //        levelData.buildIndex = indexScene;
        //        indexScene++;

        //        if(lastPath == "")
        //        {
        //            lastPath = scenePath.Replace(sceneName, "");
        //            Debug.Log(lastPath);
        //        }

        //        if(lastPath != "")
        //        {
        //            if(lastPath == scenePath.Replace(sceneName, ""))
        //            {
        //                _levelsCotegory[_levelsCotegory.Count - 1].Levels.Add(levelData);
        //            }
        //            else
        //            {
        //                _levelsCotegory.Add(new LevelsCotegory());
        //                _levelsCotegory[_levelsCotegory.Count - 1].Levels.Add(levelData);
        //            }
        //            lastPath = scenePath.Replace(sceneName, "");
        //        }
        //    }
        //}

        // Обновляем активную копию ScriptableObject в редакторе
        //EditorUtility.SetDirty(this);
        //AssetDatabase.SaveAssets();
    }

    //[ContextMenu("CountingLevels")]
    //private void CountingLevels()
    //{
    //    _countCoopLevels = _levelsCotegory.Where(p=>p.GetCotegory() == Levels.Coop).FirstOrDefault().Levels;

    //    var singleModeCotegory = _levelsCotegory.Where(p => p.GetCotegory() != Levels.Coop).ToArray();
    //    _countSingleLevels.Clear();
    //    foreach (var item in singleModeCotegory)
    //    {
    //        _countSingleLevels.AddRange(item.Levels);
    //    }
    //}
#endif
}

[Serializable]
public class LevelsCotegory
{
    [field: SerializeField] public Levels _cotegory {get; private set;}
    [field: SerializeField] public List<LevelData> Levels { get; private set; }
    [field: SerializeField] public string _nameRussian { get; private set; }
    [field: SerializeField] public string _nameEnglish {get; private set;}
    [field: SerializeField] public Sprite _icon {get; private set;}

    public string GetName(Language language) => language == Language.rus ? _nameRussian : _nameEnglish;
}

[Serializable]
public class LevelData
{

#if UNITY_EDITOR
    [field: SerializeField] public SceneAsset Scene { get; private set; }
#endif

    public string StringNameLevel = "";
}
