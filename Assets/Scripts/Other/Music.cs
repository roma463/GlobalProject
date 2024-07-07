using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music Instance { private set; get; }
    [SerializeField] private AudioSource _audioSourse;

    private void Awake()
    {
        if (Instance != null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public AudioSource GetAudioSource()
    {
        if (_audioSourse == null)
            throw new System.NullReferenceException();

        return _audioSourse;
    }
}
