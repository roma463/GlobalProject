using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Trajectory))]
public class PositionGun : MonoBehaviour
{
    public event System.Action<int> ChangedCountBullet;
    protected Vector2 Velosity;

    [SerializeField] private bool _isInfinitShoting;
    [SerializeField] protected int _countShot;
    [SerializeField] protected Teleport _teleport;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private float _forceShot;
    [SerializeField] private HoldObject _holdObject;
    [SerializeField] private LayerMask _rayCollsiion;
    [SerializeField] private AudioSource _soundShot;

    [SerializeField] private AnimationCurve _outputAnimation;
    [SerializeField] private Transform _spriteGun;

    private InputButton _inputButton;
    private Trajectory _trajectory;
    private Camera _camera;
    private bool _canShot = true;

    public virtual void Start()
    {
        _inputButton = InputButton.Instance;
        ChangedCountBullet?.Invoke(_countShot);
        _trajectory = GetComponent<Trajectory>();
        _camera = Camera.main;
    }

    public Bullet GetBullet()
    {
        return _bullet;
    }

    public virtual void Update()
    {
        if (!_canShot || GameState.Instance.IsPaused)
            return;

        Velosity = -(_gunPoint.position - _camera.ScreenToWorldPoint(Input.mousePosition)) * _forceShot;
        CreateTrajectory(Velosity);

        if (_inputButton.MouseLeft && _countShot > 0)
        {
            if (!Physics2D.OverlapCircle(_gunPoint.position, .1f, _rayCollsiion))
            {
                Shot();
            }
        }

        if (_inputButton.MouseRightStay && _holdObject.ObjectRised == false)
        {
            _holdObject.CheckHoldObject();
        }
        else if (!_inputButton.MouseRightStay && _holdObject.ObjectRised == true)
        {
            _holdObject.Throw();
        }
    }

    public bool GetInfinit()
    {
        return _isInfinitShoting;
    }

    public void GunEnable()
    {
        _canShot = true;
        UpdateText();
    }

    public void GunDisable()
    {
        _canShot = false;
    }

    public virtual void CreateTrajectory(Vector2 speed)
    {
        _trajectory.TrajectoryBullet(speed);
    }

    public void AddBullet(int count)
    {
        _countShot += count;
        UpdateText();
    }

    public virtual void Shot()
    {
        if(!_isInfinitShoting)
            _countShot--;

        var bullet = CreateBullet(_gunPoint.position);
        bullet.Init(_teleport, Velosity);
        ShotFX();
    }

    private void UpdateText()
    {
        if (_isInfinitShoting)
            return;

        ChangedCountBullet(_countShot);

    }

    public void ShotFX()
    {
        StartCoroutine(GunAnimation());
        _soundShot.Play();
        UpdateText();

        if (_countShot == 0)
        {
            _trajectory.DisableTrajectoryLine();
        }
    }

    public virtual Bullet CreateBullet(Vector2 position)
    {
        var bullet = Instantiate(_bullet, position, Quaternion.identity);
        return bullet;
    }

    private IEnumerator GunAnimation()
    {
        for (float i = 0; i < 1; i += Time.deltaTime * 10)
        {
            _spriteGun.localPosition = (Vector3)Vector2.left * (_outputAnimation.Evaluate(i)); 
            yield return null;
        }
        _spriteGun.localPosition = Vector3.zero;
    } 
}
