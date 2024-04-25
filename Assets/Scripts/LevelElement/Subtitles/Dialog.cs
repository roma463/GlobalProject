using System;
using System.Collections;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField] private Parametrs[] _parametrs;
    [SerializeField] private AudioSource _sourceVoise;
    [SerializeField] private float _dealy = .5f; 
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
        for (int i = 0; i < _parametrs.Length; i++)
        {
            var audioClip = _parametrs[i].GetAudioClip();
            _sourceVoise.clip = audioClip;
            _sourceVoise.Play();
            _viewer.ViewText(_parametrs[i].GetText());
            yield return new WaitForSeconds(audioClip.length + _dealy);
        }
    }

    public void StopDialog() 
    {

    }
}

[Serializable]
public class Parametrs
{
    [TextArea(10,30)]
    [SerializeField] private string _text;
    [SerializeField] private AudioClip _textVoise;

    public AudioClip GetAudioClip() => _textVoise;
    public string GetText() => _text;
}
