using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Dialog : MonoBehaviour
{
    [SerializeField] private Parametrs[] _parametrs;
    [SerializeField] private float _dealy = .5f;
    [SerializeField] private UnityEvent _aciton;
    private ViewerSubtitrs _viewer;
    private SaveGame _saveGame;

    private void Start()
    {
        _viewer = ViewerSubtitrs.Instance;
    }

    [Inject]
    public void Init(SaveGame saveGame)
    {
        _saveGame = saveGame;
    }

    public void StartDialog()
    {
        StartCoroutine(ChangeSubtitres());
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
