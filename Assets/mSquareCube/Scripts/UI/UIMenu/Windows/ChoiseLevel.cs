using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class ChoiseLevel : WindowUI
{
    [SerializeField] private UnityEngine.UI.Button _countinue;
    [SerializeField] private WindowUI w_watning;
    [SerializeField] private Transform _parentEpisodes;
    private SaveGame _save;

    [Inject]
    public void Construct(SaveGame save)
    {
        _save = save;
    }

    public void NewGame()
    {
        if (_save.Data.LevelSingleIndex != SaveGame.startLevelIndex)
        {
            w_watning.Show();
        }
        else
        {
            LaunchGame(SaveGame.startLevelIndex);
        }
    }

    public void ResetSaveLevel()
    {
        _save.ClearGameProgress();
        LaunchGame(_save.Data.LevelSingleIndex);
    }

    public void Countinue()
    {
        var indexLevel = _save.Data.LevelSingleIndex;
        LaunchGame(indexLevel);
    }

    public void LaunchGame(int indexLevel)
    {
        SceneManager.LoadScene(indexLevel);
    }
}
