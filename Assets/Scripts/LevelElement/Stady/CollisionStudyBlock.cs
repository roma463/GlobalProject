using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

[RequireComponent(typeof(Rigidbody2D))]
public class CollisionStudyBlock : MonoBehaviour
{
    [SerializeField] private bool _isImage;
    [SerializeField] private Texture2D _sprite;
    [SerializeField] private RenderTexture _texture;
    [SerializeField] private VideoClip _clip;
    [TextArea(10,10)]
    [SerializeField] private string[] _textStudy;
    [SerializeField] private string _nameStudy;
    [SerializeField] private UnityEvent _closedWindow;
    private StudyWindow _studyWindow;

    private void Start()
    {
        _studyWindow = StudyWindow.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.root.TryGetComponent(out PlayerMove playerMove))
        {
            if (!_isImage)
            {
                ActiveWindowStudy();
            }
            else
            {
                _studyWindow.InitWindow(_sprite, _textStudy, _nameStudy, _closedWindow);
                gameObject.SetActive(false);
            }
        }
    }

    public void ActiveWindowStudy() 
    {
            _studyWindow.InitWindow(_texture, _clip, _textStudy, _nameStudy, _closedWindow);
            gameObject.SetActive(false);
    }
}
