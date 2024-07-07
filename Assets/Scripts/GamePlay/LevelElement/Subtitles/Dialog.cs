using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{
    [SerializeField] private Parametrs[] _parametrs;
    [SerializeField] private float _dealy = .5f;
    [SerializeField] private UnityEvent _aciton;
    private ViewerSubtitrs _viewer;

    private void Start()
    {
        _viewer = ViewerSubtitrs.Instance;
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
            _viewer.ViewText(_parametrs[i].GetText());
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
    [SerializeField] private float _timeText;

    public float GetTimeText() => _timeText;

    public string GetText() => _text;
}
