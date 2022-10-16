using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiForOneLevel : GameUi
{
    [SerializeField] private Animator _choisePlayer;
    [SerializeField] private Animator _animSquare;
    [SerializeField] private Animator _animCamera;
    [SerializeField] private Animator _animButton;
    public override void Awake()
    {
        base.Awake();
    }

    public void FirstStart()
    {
        _animSquare.SetTrigger("_isDisperse");
        //_animCamera.SetTrigger("_isMoveCamera");
        _choisePlayer.SetTrigger("ChoisePlayer");
        _animButton.SetTrigger("_isMoveButton");
    }

    public override void Start()
    {
    }
    public override void NextLevel()
    {
        base.NextLevel();
    }
    public override void Restart()
    {
        
    }
    public override void Win()
    {
       
    }
}
