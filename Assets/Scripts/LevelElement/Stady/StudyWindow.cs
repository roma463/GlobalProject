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
    [SerializeField] private TextMeshProUGUI _discription;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private RawImage _image;
    private GameState _gameState;
    private UnityEvent _closedWindow;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _gameState = GameState.Instance;
    }

    private IEnumerator ActiveWindow()
    {
        _gameState.PauseGame();
        _video.enabled = true;
        _video.Play();
        yield return new WaitUntil(() => _video.isPlaying);
        _elementWindow.SetActive(true);
    }

    public void InitWindow(RenderTexture texture, VideoClip clip, string text, string name, UnityEvent closedWindow)
    {
        _video.targetTexture = texture;
        _video.clip = clip;
        _discription.text = text;
        _image.texture = texture;
        _name.text = name;
        _closedWindow = closedWindow;
        StartCoroutine(ActiveWindow());
    }

    public void ClickOk()
    {
        _gameState.PauseGame();
        _elementWindow.SetActive(false);
        _closedWindow?.Invoke();
        _video.Stop();
    }
}
