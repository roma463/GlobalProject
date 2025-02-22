using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class MenuUi : MonoBehaviour
{
    public static MenuUi Instance { get; private set; }
    [SerializeField] private WindowUI w_basic;

    [SerializeField] private Animator _animationMenu;
    [SerializeField] private ConnectionToServer _connetionToServer;
    [SerializeField] private Button _continue;
    [SerializeField] private Button _back;
    [SerializeField] private PopupMessageView _warningPopup;

    [SerializeField] private LanguageTextData _coopWarning;
    [SerializeField] private LanguageTextData _resetDataWarning;

    private List<WindowUI> _listOpenWindow = new List<WindowUI>();
    private SaveGame _saveGame;

    [Inject]
    public void Construct(SaveGame save)
    {
        _saveGame = save;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start ()
    {
        if (_saveGame.Data.LevelSingleIndex != SaveGame.startLevelIndex)
            _continue.interactable = true;
        _listOpenWindow.Add(w_basic);
    }

    public void OnCoopClick()
    {
        _warningPopup.Setup(new MessagePopup
        {
            message = _coopWarning.GetText(_saveGame.Data.CurrentLanguage),
            cancelButton = () => _warningPopup.Hide(),
        });
    }

    public void NewGame()
    {
        if (_saveGame.Data.LevelSingleIndex != SaveGame.startLevelIndex)
        {
            _warningPopup.Setup(new MessagePopup
            {
                message = _resetDataWarning.GetText(_saveGame.Data.CurrentLanguage),
                continueButton = () => LaunchGame(SaveGame.startLevelIndex),
                cancelButton = () => _warningPopup.Hide(),
            });
        }
        else
        {
            LaunchGame(SaveGame.startLevelIndex);
        }
    }

    public void LaunchGame(int indexLevel)
    {
        SceneManager.LoadScene(indexLevel);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        _connetionToServer.StartCoroutine(_connetionToServer.Disconection(LoadScene));
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenWindow(WindowUI window)
    {
        _listOpenWindow.Add(window);
        ActiveWindow(window, _listOpenWindow[_listOpenWindow.Count - 2]);
        StateBackButton();

    }

    public void ClosedWindow()
    {
        var lastOpenWindow = _listOpenWindow[_listOpenWindow.Count - 1];
        _listOpenWindow.Remove(lastOpenWindow);
        ActiveWindow(_listOpenWindow[_listOpenWindow.Count - 1], lastOpenWindow);
        StateBackButton();
    }

    private void StateBackButton()
    {
        if(_listOpenWindow.Count > 1)
            _back.gameObject.SetActive(true);
        else
            _back.gameObject.SetActive(false);
    }

    private void ActiveWindow(WindowUI windowOpen, WindowUI windowClosed)
    {
        windowOpen.Show();
        windowClosed.Hide();
    }

}

[Serializable]
public class LanguageTextData
{
    [TextArea(3, 10)]
    [SerializeField] private string _rassian;
    [TextArea(3, 10)]
    [SerializeField] private string _english;

    public string GetText(Language language) => language == Language.rus ? _rassian : _english;
}
