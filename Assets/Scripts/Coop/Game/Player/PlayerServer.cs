using Photon.Pun;
using UnityEngine;

public class PlayerServer : MonoBehaviour
{
    public static PlayerServer Instance { private set; get; }
    [SerializeField] private PositionGunServer _positionGun; 
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private LineRenderer _lineTrajectory;
    [SerializeField] private SpriteRenderer _playerSprite;
    [SerializeField] private Gradient _lineColor;
    [SerializeField] private Color _colorPlayer;
    [SerializeField] private GameObject _isMineIndicator;
    [SerializeField] private Rigidbody2D _rigidbody;

    private void Awake()
    {
        if(_photonView.IsMine)
            Instance = this;
    }

    private void Start()
    {
        if (!_photonView.IsMine)
        {
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector3.zero;
            _playerSprite.sortingOrder = 45;
            _lineTrajectory.colorGradient = _lineColor;
            _isMineIndicator.SetActive(false);
            _playerSprite.color = _colorPlayer;
        }
    }

    public PhotonView GetPhotonView()
    {
        return _photonView;
    }

    #region RPC

    [PunRPC]
    public void LoadLevel(int idScene)
    {
        PhotonNetwork.LoadLevel(idScene);
    }

    [PunRPC]
    public void ShotPlayer()
    {
        _positionGun.ShotRemotePlayer();
    }
    #endregion
}
