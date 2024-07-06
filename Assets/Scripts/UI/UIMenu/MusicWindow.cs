using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicWindow : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource; 
    [SerializeField] private MusicItem[] _musicItems;
    [SerializeField] private Slider _lineTime;
    [SerializeField] private TextMeshProUGUI _nameTrack;
    [SerializeField] private TextMeshProUGUI _durationTrack;
    [SerializeField] private TextMeshProUGUI _currentTime;
    private bool _isPlaying = true;
    private int _currentTrackIndex = 0;

    private void Start()
    {
        Play(_musicItems[_currentTrackIndex]);
    }

    private void Play(MusicItem item)
    {
        _audioSource.clip = item.GetMusicClip();

        _nameTrack.text = item.GetName();
        _durationTrack.text = FormatTime(item.GetMusicClip().length);

        _lineTime.maxValue = _audioSource.clip.length;

        _audioSource.Play();
        _isPlaying = true;
    }

    private void Update()
    {
        if (!_isPlaying)
            return;

        _lineTime.value = _audioSource.time;
        _currentTime.text = FormatTime(_audioSource.time);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void RightButton()
    {
        SwitchingTrack(true);
    }

    public void LeftButton()
    {
        SwitchingTrack(false);
    }

    private void SwitchingTrack(bool rightOffset)
    {
        _currentTrackIndex += rightOffset? 1 : -1;
        if (_currentTrackIndex < 0)
            _currentTrackIndex = _musicItems.Length - 1;
        else if (_currentTrackIndex > _musicItems.Length - 1)
            _currentTrackIndex = 0;

        Play(_musicItems[_currentTrackIndex]);

    }

    public void StateTrackButton()
    {
        _isPlaying = !_isPlaying;

        if (_isPlaying)
            _audioSource.Play();
        else
        {
            _audioSource.Pause();
        }
    }
}

[Serializable]
public class MusicItem
{
    [SerializeField] private AudioClip _music;
    [SerializeField] private string _name;

    public AudioClip GetMusicClip() => _music;
    public string GetName() => _name;
}
