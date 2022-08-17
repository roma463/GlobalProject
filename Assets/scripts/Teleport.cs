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
    [SerializeField] private Transform _pointUP;
    [SerializeField] private Transform _pointDown;
    [SerializeField] private Transform _pointLeft;
    [SerializeField] private Transform _pointRight;
    [SerializeField] private HoldObject _holdObject;
    [SerializeField] private ParticlesPlayer _playerParticles;
    [SerializeField] private Transform _textPoint;
    [SerializeField] private AudioSource _teleportSound;
    private void Awake()
    {
        _textPoint.parent = null;
        GravityScale = 1;
        GlobalTP = this;
    }
    public void Player(Offset of, Vector2 newPosition)
    {
        _playerParticles.Play(ParticlesPlayer.ViewParticle.TelePort);

        Vector2 offset = Vector2.zero;
        if (of == Offset.down)
        {
            offset = transform.position - _pointDown.position;
        }
        else if (of == Offset.up)
        {
            offset = transform.position - _pointUP.position;
        }
        if (of == Offset.left)
        {
            offset = transform.position - _pointLeft.position;
        }
        else if (of == Offset.right)
        {
            offset = transform.position - _pointRight.position;
        }
        offset *= GravityScale;
        transform.position = newPosition - offset;
        _textPoint.position = newPosition - offset;
        _playerParticles.Play(ParticlesPlayer.ViewParticle.TelePort);
        _holdObject.TeleportCurrentUseObject(transform.position);
        PlayerMove.GlobalPlayer.UnplugJump();
        _teleportSound.Play();
    }
    public void UpdateDataGraviryScale(Rigidbody2D rigidbody2D)
    {
        GravityScale = (int)transform.localScale.y;
    }
}
