using UnityEngine;
using UnityEngine.SceneManagement;

public class UiOneLevel : GameUi
{
    [SerializeField] private Animator _choisePlayer;
    [SerializeField] private Animator _animSquare;
    [SerializeField] private Animator _animCamera;
    [SerializeField] private Animator _animButton;
    [SerializeField] private Animator _animButtonChoise;
    [SerializeField] private int _oneLevelIndex;
    public override void Awake()
    {
        base.Awake();
    }

    public void FirstStart()
    {
        _animSquare.SetTrigger("_isDisperse");
        _animButton.SetTrigger("_isMoveButton");
        _animButtonChoise.SetTrigger("Choise");
    }

    public override void Start()
    {
    }
    public override void NextLevel()
    {
        SceneManager.LoadScene(_oneLevelIndex+1);
    }
    public void OnChoise()
    {
        _animCamera.SetTrigger("_isMoveCamera");
        _choisePlayer.SetTrigger("ChoisePlayer");
        _animButtonChoise.SetTrigger("ChoiseEnd");
    }
    public override void Restart()
    {
        SceneManager.LoadScene(_oneLevelIndex);
        StopReadClick(false);
    }
    public override void WinWindow()
    {
        _animator.SetTrigger("Finish");
    }
    public override void StopReadClick(bool state)
    {
        base.StopReadClick(state);
    }
}
