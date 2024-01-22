using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Trajectory))]
public class PositionGun : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private float _forceShot;
    [SerializeField] private HoldObject _holdObject;
    [SerializeField] private LayerMask _rayCollsiion;
    [SerializeField] private InputButton _inputButton;
    [SerializeField] private GameUi _stateUi;
    [SerializeField] private TextMesh _textCountShot;
    [SerializeField] private int _countShot;
    [SerializeField] private Color _zeroBullet;
    [SerializeField] private AudioSource _soundShot;

    [SerializeField] private AnimationCurve _outputAnimation;
    [SerializeField] private Transform _spriteGun;
    private Trajectory _trajectory;
    private Camera _camera;
    private Color _startColor;

    private void Start()
    {
        _textCountShot.text = _countShot.ToString();
        _trajectory = GetComponent<Trajectory>();
        _camera = Camera.main;
        _startColor = _textCountShot.color;
    }
    private void Update()
    {
        var speed = -(_gunPoint.position - _camera.ScreenToWorldPoint(Input.mousePosition)) * _forceShot;
        _trajectory.TrajectoryBullet(speed);

        if (_inputButton.MouseLeft && _countShot > 0)
        {
            StartCoroutine(GunAnimation());
            Shot(speed);
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
    public void AddBullet(int countAdd)
    {
        _countShot += countAdd;
        _textCountShot.text = _countShot.ToString();
        _textCountShot.color = _startColor;
        _trajectory.EnableTranjectoryLine();
    }
    public void OnDisableGun()
    {
        _trajectory.DisableTranjectoryLine();
        _textCountShot.color = new Color(0, 0, 0, 0);
        _spriteGun.gameObject.SetActive(false);
    }
    public void OnEnableGun()
    {
        _trajectory.EnableTranjectoryLine();
        _textCountShot.color = _startColor;
        _spriteGun.gameObject.SetActive(true);
    }
    private void Shot(Vector2 speedBullet)
    {
        if (_countShot > 0)
        {
            if (!Physics2D.OverlapCircle(_gunPoint.position, .1f, _rayCollsiion))
            {
                _countShot--;
                _textCountShot.text = _countShot.ToString();
                if(_countShot == 0)
                {
                    _textCountShot.color = _zeroBullet;
                    _trajectory.DisableTranjectoryLine();
                    
                }
                var bullet = Instantiate(_bullet, _gunPoint.position, Quaternion.identity);
                var rigidbodyBullet = bullet.GetComponent<Rigidbody2D>();
                rigidbodyBullet.gravityScale *= Teleport.GlobalTP.GravityScale;
                rigidbodyBullet.AddForce(speedBullet, ForceMode2D.Impulse);
                _soundShot.Play();
            }
        }
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