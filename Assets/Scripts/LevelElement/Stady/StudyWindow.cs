using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public class StudyWindow : MonoBehaviour
{
    public static StudyWindow Instance {  get; private set; }
    [SerializeField] private GameObject _elementWindow;
    [SerializeField] private VideoPlayer _video;
    [SerializeField] private TextMeshProUGUI _discriptionView;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private UnityEngine.UI.Button _father;
    [SerializeField] private RawImage _image;
    private GameState _gameState;
    private UnityEvent _closedWindow;
    private string[] _discription;
    private int _numberPageDescription = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _gameState = GameState.Instance;
    }

    private IEnumerator StartedVidioPlayer()
    {
        _video.enabled = true;
        _video.Play();
        yield return new WaitUntil(() => _video.isPlaying);
        _elementWindow.SetActive(true);
    }

    public void InitWindow(RenderTexture texture, VideoClip clip, string[] text, string name, UnityEvent closedWindow)
    {
        _video.targetTexture = texture;
        _video.clip = clip;
        _image.texture = texture;
        Activate(text, name, closedWindow);
        StartCoroutine(StartedVidioPlayer());
    }

    public void InitWindow(Texture2D sprite, string[] text, string name, UnityEvent closedWindow)
    {
        _image.texture = sprite;
        Activate(text, name, closedWindow);
        _elementWindow.SetActive(true);
    }

    private void Activate(string[] text, string name, UnityEvent closedWindow)
    {
        _gameState.PauseGame();
        _discription = text;
        _discriptionView.text = _discription[_numberPageDescription];
        _name.text = name;
        _closedWindow = closedWindow;
        if (_discription.Length > 1)
        {
            _father.gameObject.SetActive(true);
        }
    }

    public void Futther()
    {
        _numberPageDescription++;
        _discriptionView.text = _discription[_numberPageDescription];
        if(_numberPageDescription == _discription.Length - 1)
        {
            _father.gameObject.SetActive(false);
        }
    }

    public void ClickOk()
    {
        _gameState.PauseGame();
        _elementWindow.SetActive(false);
        _closedWindow?.Invoke();
        _video.Stop();
    }
}
