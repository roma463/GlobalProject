using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

public class MenuSettings : WindowUI
{
    public static MenuSettings Instance { private set; get; }
    public event Action LanguageUpdate;
    [SerializeField] private GameObject[] _languageObject;
    [SerializeField] private AudioMixer _musicMixer, _soundMixer;
    [SerializeField] private Toggle _postProcessing;
    [SerializeField] private Slider _musicSlider, _soundSlider;
    private SaveGame _save;
    private int _currentLenguage = 0;
    private const string KEY_AUDIO_VOLUME = "Volume";

    [Inject]
    public void Construct(SaveGame save)
    {
        _save = save;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _currentLenguage = (int)(_save.Data.CurrentLanguage);
        ChangeLanguage();

        _postProcessing.isOn = _save.Data.Settings.PostProcessing;
        _musicSlider.value = _save.Data.Settings.VolumeMusic;
        _soundSlider.value = _save.Data.Settings.VolumeSound;
    }
    private void ChangeLanguage()
    {
        for (int i = 0; i < _languageObject.Length; i++)
        {
            _languageObject[i].SetActive(false);
        }
        _languageObject[_currentLenguage].SetActive(true);
        _save.Data.CurrentLanguage = (Language)_currentLenguage;
        LanguageUpdate?.Invoke();
        _save.SaveData();
    }

    public void ChangeSoundMixer()
    {
        _soundMixer.SetFloat(KEY_AUDIO_VOLUME, _soundSlider.value);
        _save.Data.Settings.VolumeSound = _soundSlider.value;
        _save.SaveData();
    }
    public void ChangeMusicMixer()
    {
        _musicMixer.SetFloat(KEY_AUDIO_VOLUME, _musicSlider.value);
        _save.Data.Settings.VolumeMusic = _musicSlider.value;
        _save.SaveData();
    }

    public void ClickToggle()
    {
        _save.Data.Settings.PostProcessing = _postProcessing.isOn;
        PostProcessingSettings.Instance.UpdateChanged(_postProcessing.isOn);
        _save.SaveData();
    }

    public void ButtonLeftLanguage()
    {
        _currentLenguage--;
        ClampIndexLanguage();
        ChangeLanguage();
    }

    public void ButtonRightLangeage()
    {
        _currentLenguage++;
        ClampIndexLanguage();
        ChangeLanguage();

    }

    private void ClampIndexLanguage()
    {
        _currentLenguage = Mathf.Clamp(_currentLenguage, 0, _languageObject.Length - 1);
    }
}
