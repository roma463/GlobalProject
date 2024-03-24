using Photon.Pun;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Trajectory))]
public class PositionGun : MonoBehaviour
{
    protected Vector2 Velosity;
    [SerializeField] protected Teleport _teleport;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private float _forceShot;
    [SerializeField] private HoldObject _holdObject;
    [SerializeField] private LayerMask _rayCollsiion;
    [SerializeField] private InputButton _inputButton;
    [SerializeField] private TextMesh _textCountShot;
    [SerializeField] private int _countShot;
    [SerializeField] private Color _zeroBullet;
    [SerializeField] private AudioSource _soundShot;

    [SerializeField] private AnimationCurve _outputAnimation;
    [SerializeField] private Transform _spriteGun;
    private Trajectory _trajectory;
    private Camera _camera;
    private Color _startColor;

    public virtual void Start()
    {
        _textCountShot.text = _countShot.ToString();
        _trajectory = GetComponent<Trajectory>();
        _camera = Camera.main;
        _startColor = _textCountShot.color;
    }

    public Bullet GetBullet()
    {
        return _bullet;
    }

    public virtual void Update()
    {
        Velosity = -(_gunPoint.position - _camera.ScreenToWorldPoint(Input.mousePosition)) * _forceShot;
        CreateTranjectory(Velosity);
        if (_inputButton.MouseLeft && _countShot > 0)
        {
            Shot(Velosity);
        }

        if (_inputButton.MouseRightStay && _holdObject.ObjectRised == false)
        {
            _holdObject.GameobjectKeep();
        }
        else if (!_inputButton.MouseRightStay && _holdObject.ObjectRised == true)
        {
            _holdObject.Throw();
        }
    }

    public virtual void CreateTranjectory(Vector2 speed)
    {
        _trajectory.TrajectoryBullet(speed);
    }

    public void AddBullet(int countAdd)
    {
        _countShot += countAdd;
        _textCountShot.text = _countShot.ToString();
        _textCountShot.color = _startColor;
        _trajectory.EnableTrajectoryLine();
    }

    public void OnDisableGun()
    {
        _trajectory.DisableTrajectoryLine();
        _textCountShot.color = new Color(0, 0, 0, 0);
        _spriteGun.gameObject.SetActive(false);
    }

    public void OnEnableGun()
    {
        _trajectory.EnableTrajectoryLine();
        _textCountShot.color = _startColor;
        _spriteGun.gameObject.SetActive(true);
    }
    private void Shot(Vector2 speedBullet)
    {
        if (!Physics2D.OverlapCircle(_gunPoint.position, .1f, _rayCollsiion))
        {
            _countShot--;
            _textCountShot.text = _countShot.ToString();
            if(_countShot == 0)
            {
                _textCountShot.color = _zeroBullet;
                _trajectory.DisableTrajectoryLine();
            }
            StartCoroutine(GunAnimation());
            var bullet = CreateBullet(_gunPoint.position);
            bullet.Init(_teleport, speedBullet);
            _soundShot.Play();
        }
    }



    public virtual Bullet CreateBullet(Vector2 position)
    {
        return Instantiate(_bullet, position, Quaternion.identity);
    }
    private IEnumerator GunAnimation()
    {
        for (float i = 0; i < 1; i+= Time.deltaTime* 10)
        {
            _spriteGun.localPosition = (Vector3)Vector2.left * (_outputAnimation.Evaluate(i)); 
            yield return null;
        }
        _spriteGun.localPosition = Vector3.zero;
    } 
}
