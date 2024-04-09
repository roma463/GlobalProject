using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StudyWindow : MonoBehaviour
{
    public static StudyWindow Instance {  get; private set; }
    [SerializeField] private GameObject _elementWindow;
    [SerializeField] private VideoPlayer _video;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private RawImage _image;
    private GameState _gameState;

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

    public void InitWindow(RenderTexture texture, VideoClip clip, string text)
    {
        _video.targetTexture = texture;
        _video.clip = clip;
        _text.text = text;
        _image.texture = texture;
        StartCoroutine(ActiveWindow());
    }

    public void ClickOk()
    {
        _gameState.PauseGame();
        _elementWindow.SetActive(false);
        _video.Stop();
    }
}
