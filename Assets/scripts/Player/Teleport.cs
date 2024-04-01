using UnityEngine;

public class Teleport : MonoBehaviour
{
    public enum Offset
    {
        right,
        left,
        up,
        down,
    }
    public static Teleport GlobalTP { get; private set; }
    public int GravityScale { get; private set; }
    public event System.Action Effects_E;
    [SerializeField] private Transform _pointUP;
    [SerializeField] private Transform _pointDown;
    [SerializeField] private Transform _pointLeft;
    [SerializeField] private Transform _pointRight;

    [SerializeField] private HoldObject _holdObject;
    [SerializeField] private Transform _textPoint;
    [SerializeField] private AudioSource _teleportSound;
    private ShockWavePositions _shockWave;
    private Coroutine _shockWaveCorutine;
    private PlayerMove _playerMove;
    
    private void Awake()
    {
        _textPoint.parent = null;
        GravityScale = 1;
        GlobalTP = this;
    }

    private void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
        SetShockWave(ShockWavePositions.Instance);
    }

    public void SetShockWave(ShockWavePositions shockWavePositions)
    {
        _shockWave = shockWavePositions;
    }
    public void Player(Offset normalSurface, Vector2 newPosition)
    {
        Effects_E?.Invoke();

        Vector2 offset = Vector2.zero;
        if (normalSurface == Offset.down)
        {
            offset = transform.position - _pointDown.position;
        }
        else if (normalSurface == Offset.up)
        {
            offset = transform.position - _pointUP.position;
        }
        if (normalSurface == Offset.left)
        {
            offset = transform.position - _pointLeft.position;
        }
        else if (normalSurface == Offset.right)
        {
            offset = transform.position - _pointRight.position;
        }

        offset *= GravityScale;
        transform.position = newPosition - offset;
        _textPoint.position = newPosition - offset;
        Effects_E?.Invoke();
        _holdObject.TeleportCurrentUseObject(transform.position);
        //_playerMove.UnplugJump();
        _teleportSound.Play();
        if(_shockWaveCorutine != null)
            StopCoroutine(_shockWaveCorutine);
        _shockWaveCorutine = StartCoroutine(_shockWave.Teleportation(transform.position));
    }
    public void UpdateDataGraviryScale(Rigidbody2D rigidbody2D)
    {
        GravityScale = (int)transform.localScale.y;
    }
}
