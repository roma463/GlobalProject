using Photon.Pun;
using UnityEngine;

public class PlayerServer : MonoBehaviour
{
    [SerializeField] private PositionGunServer _positionGun; 
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private LineRenderer _lineTrajectory;
    [SerializeField] private SpriteRenderer _playerSprite;
    [SerializeField] private Gradient _lineColor;
    [SerializeField] private Color _colorPlayer;
    [SerializeField] private GameObject _isMineIndicator;
    [SerializeField] private Rigidbody2D _rigidbody;

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

        if (PhotonNetwork.IsMasterClient && _photonView.IsMine)
        {
            ((GameUISeriver)GameUi.Instance).InitPhoton(_photonView);
        }
    }

    [PunRPC]
    public void ShotPlayer()
    {
        _positionGun.ShotRemotePlayer();
    }
}
