using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class ChoiseLevel : WindowUI
{
    [SerializeField] private UnityEngine.UI.Button _countinue;
    [SerializeField] private WindowUI w_watning;
    [SerializeField] private LevelList _level;
    [SerializeField] private Transform _parentEpisodes;
    private SaveGame _save;

    [Inject]
    public void Construct(SaveGame save)
    {
        _save = save;
    }

    private void Start()
    {
        if(_save.Saves.LevelSingleIndex != 0)
            _countinue.interactable = true;
    }

    public void NewGame()
    {
        if (_save.Saves.LevelSingleIndex != 0)
        {
            w_watning.Show();
        }
        else
        {
            LaunchGame(_level.GetCurrentSceneSingleLevel(0));
        }
    }

    public void ResetSaveLevel()
    {
        _save.Saves.LevelSingleIndex = 0;
        _save.SaveData();
        LaunchGame(_level.GetCurrentSceneSingleLevel(0));
    }

    public void Countinue()
    {
        var indexLevel = _level.GetCurrentSceneSingleLevel(_save.Saves.LevelSingleIndex);
        LaunchGame(indexLevel);
    }

    public void LaunchGame(int indexLevel)
    {
        SceneManager.LoadScene(indexLevel);
    }
}
