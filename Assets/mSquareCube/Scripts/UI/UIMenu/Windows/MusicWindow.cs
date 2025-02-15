using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicWindow : WindowUI
{
    public static MusicWindow Instance { get; private set; }

    public event Action<bool> OnPlay;
    [SerializeField] private MusicItem[] _musicItems;
    [SerializeField] private Slider _lineTime;
    [SerializeField] private TextMeshProUGUI _nameTrack;
    [SerializeField] private TextMeshProUGUI _durationTrack;
    [SerializeField] private TextMeshProUGUI _currentTime;

    private static int _currentTrackIndex = 0;
    private AudioSource _audioSource; 
    private bool _isPlaying = true;

    private void Awake()
    {
        Instance = this;
    }

    public override void Start()
    {
        base.Start();
        _audioSource = Music.Instance.GetAudioSource();
    }

    public override void Show()
    {
        var item = _musicItems[_currentTrackIndex];
        if (_audioSource.clip == null)
            SwitchTrack(item);
        else
            InitializeParametrs(item);
        base.Show();
    }

    private async void SwitchTrack(MusicItem item)
    {
        await LoadClipInAudioSource(item);
        _audioSource.Play();
        _isPlaying = true;
        OnPlay.Invoke(_isPlaying);

        InitializeParametrs(item);
    }

    private async Task LoadClipInAudioSource(MusicItem item)
    {
        var clip = await Task.Run(() => item.GetMusicClip());
        _audioSource.clip = await Task.Run(() => clip);
    }

    private void InitializeParametrs(MusicItem item)
    {
        _nameTrack.text = item.GetName();
        _durationTrack.text = FormatTime(item.GetMusicClip().length);
        _lineTime.maxValue = _audioSource.clip.length;
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
        SwitchingIndexTrack(true);
    }

    public void LeftButton()
    {
        SwitchingIndexTrack(false);
    }

    private void SwitchingIndexTrack(bool rightOffset)
    {
        _currentTrackIndex += rightOffset? 1 : -1;
        if (_currentTrackIndex < 0)
            _currentTrackIndex = _musicItems.Length - 1;
        else if (_currentTrackIndex > _musicItems.Length - 1)
            _currentTrackIndex = 0;

        SwitchTrack(_musicItems[_currentTrackIndex]);

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

        OnPlay.Invoke(_isPlaying);
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
