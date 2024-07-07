using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiseLevel : WindowUI
{
    [SerializeField] private UnityEngine.UI.Button _countinue;
    [SerializeField] private UnityEngine.UI.Button _levelChoise;
    [SerializeField] private WindowUI w_watning;
    [SerializeField] private LevelList _level;
    [SerializeField] private Episode _prefab;
    [SerializeField] private Transform _parentEpisodes;
    private SaveGame _saveGame;

    private void Start()
    {
        _saveGame = SaveGame.Instance;
        if(_saveGame.Saves.LevelSingleIndex != 0)
        {
            _countinue.interactable = true;
            _levelChoise.interactable = true;
        }
            CreationEpisodes();
    }

    private void CreationEpisodes()
    {
        var levelCorigories = _level.GetLevelsCotegories();

        foreach (var item in levelCorigories)
        {
            Episode episode = Instantiate(_prefab, _parentEpisodes);
            episode.Init(item, this);
        }
    }

    public void NewGame()
    {
        if (_saveGame.Saves.LevelSingleIndex != 0)
        {
            w_watning.Activate();
        }
        else
        {
            LaunchGame(_level.GetCurrentSceneSingleLevel(0));
        }
    }

    public void ResetSaveLevel()
    {
        _saveGame.Saves.LevelSingleIndex = 0;
        _saveGame.SaveData();
        LaunchGame(_level.GetCurrentSceneSingleLevel(0));
    }

    public void Countinue()
    {
        var indexLevel = _level.GetCurrentSceneSingleLevel(_saveGame.Saves.LevelSingleIndex);
        LaunchGame(indexLevel);
    }

    public void LaunchGame(int indexLevel)
    {
        SceneManager.LoadScene(indexLevel);
    }

    public void SelectEpisode()
    {

    }
}
