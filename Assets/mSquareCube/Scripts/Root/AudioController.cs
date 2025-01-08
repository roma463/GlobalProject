using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioController _instance;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioSource _souseWorkdFx;
    [SerializeField] public AudioClip[] _clipForText;
    [SerializeField] private float _pitchOffset = .1f;

    private float _startPitch;

    private void Awake()
    {
        _instance = this;
        _startPitch = _source.pitch;
    }

    public void TextViewStart()
    {
        _source.pitch = _startPitch + Random.Range(-_pitchOffset, _pitchOffset);
        _source.PlayOneShot(_clipForText[Random.Range(0, _clipForText.Length)]);
    }

    public void PlayHit(AudioClip clip)
    {
        _souseWorkdFx.PlayOneShot(clip);
    }

    public static void ViewText()
    {
        _instance.TextViewStart();
    }

    public static void StartPlayHit(AudioClip clip)
    {
        _instance.PlayHit(clip);
    }
}
