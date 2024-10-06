using GamePlay.Player;
using Photon.Pun;
using UnityEngine;

public class DisablingComponent : MonoBehaviour
{
    [SerializeField] private PhotonView _photonView;

    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private RotateArm _rotateAem;

    private void Awake()
    {
        if (_photonView.IsMine)
            return;
        Destroy((Component)_playerMove);
        Destroy((Component)_rotateAem);
    }
}
