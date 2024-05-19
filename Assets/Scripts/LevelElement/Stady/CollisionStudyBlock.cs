using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class CollisionStudyBlock : MonoBehaviour
{
    [SerializeField] private RenderTexture _texture;
    [SerializeField] private UnityEvent _closedWindow;
    [SerializeField] private VideoClip _clip;
    [TextArea(10,10)]
    [SerializeField] private string _textStudy;
    [SerializeField] private string _nameStudy;
    private BoxCollider2D _boxCollider;
    private StudyWindow _studyWindow;

    private void OnValidate()
    {
        if (_boxCollider != null)
            return;
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        _studyWindow = StudyWindow.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.root.TryGetComponent(out PlayerMove playerMove))
        {
            _studyWindow.InitWindow(_texture, _clip, _textStudy, _nameStudy, _closedWindow);
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 postion = _boxCollider.offset + (Vector2)transform.position;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(postion, _boxCollider.size);
    }
}
