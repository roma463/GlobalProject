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
    [SerializeField] private int _countShot;
    private Trajectory _trajectory;
    private Camera _camera;

    private void Start()
    {
        _stateUi.DecreaseCountShot(_countShot);
        _trajectory = GetComponent<Trajectory>();
        _camera = Camera.main;
    }
    private void Update()
    {
        var speed = -(_gunPoint.position - _camera.ScreenToWorldPoint(Input.mousePosition)) * _forceShot;
        _trajectory.TrajectoryBullet(speed);

        if (_inputButton.MouseLeft)
        {
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
    private void Shot(Vector2 speedBullet)
    {
        if (_countShot > 0)
        {
            if (!Physics2D.OverlapCircle(_gunPoint.position, .1f, _rayCollsiion))
            {
                _countShot--;
                _stateUi.DecreaseCountShot(_countShot);
                var bullet = Instantiate(_bullet, _gunPoint.position, Quaternion.identity);
                var rigidbodyBullet = bullet.GetComponent<Rigidbody2D>();
                rigidbodyBullet.AddForce(speedBullet, ForceMode2D.Impulse);
            }
        }
        else
        {
            _stateUi.Lost();
        }
    }
}
