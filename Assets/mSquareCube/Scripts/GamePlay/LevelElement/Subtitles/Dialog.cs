using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.Events;
using WebSocketSharp;
using Zenject;

public class Dialog : MonoBehaviour
{
    public static List<string> _showdDialogs = new List<string>();

    [SerializeField] private string _id;
    [SerializeField] private Parametrs[] _parametrs;
    [SerializeField] private float _dealy = .5f;
    [SerializeField] private UnityEvent _aciton;
    [SerializeField] private bool _isShowdRepeat = false;

    private ViewerSubtitrs _viewer;
    private SaveGame _saveGame;

    private void OnValidate()
    {
        if(Application.IsPlaying(this))
        {
            print("Dialog is plaint");
            return;
        }
        print("Dialog validate");

        _id = Guid.NewGuid().ToString();
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
}

    private void Start()
    {
        _viewer = ViewerSubtitrs.Instance;
    }

    [Inject]
    public void Init(SaveGame saveGame)
    {
        _saveGame = saveGame;
    }

    public static void ClearShowedDialogs()
    {
        _showdDialogs.Clear();
    }

    public void StartDialog()
    {
        if(_isShowdRepeat || !_showdDialogs.Contains(_id))
        {
            foreach (var item in _showdDialogs)
            {
                print("AAAAAAA " + item);
            }
            StartCoroutine(ChangeSubtitres());
            _showdDialogs.Add(_id);
        }
        else
        {
            _aciton?.Invoke(); 
        }
    }

    private IEnumerator ChangeSubtitres()
    {
        _viewer.ActivateWindow();
        for (int i = 0; i < _parametrs.Length; i++)
        {
            _viewer.ViewText(_parametrs[i].GetText(_saveGame.Data.CurrentLanguage));
            yield return new WaitUntil(() => !_viewer.IsVieweble);
            yield return new WaitForSeconds(_parametrs[i].GetTimeText());
        }
        yield return new WaitForSeconds(_dealy);
        _viewer.DeactivateWindow();
        _aciton?.Invoke();
    }

    public void StopDialog() 
    {

    }
}

[Serializable]
public class Parametrs
{
    [TextArea(3,10)]
    [SerializeField] private string _text;
    [TextArea(3, 10)]
    [SerializeField] private string _translate;
    [SerializeField] private float _timeText;

    public float GetTimeText() => _timeText;

    public string GetText(Language language) => language == Language.rus ? _text : _translate;
}
