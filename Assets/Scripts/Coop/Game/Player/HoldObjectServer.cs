using Photon.Pun;
using UnityEngine;

public class HoldObjectServer : HoldObject
{
    [SerializeField] private PlayerServer _playerServer;
    private PhotonView _photonView;

    private void Start()
    {
        _photonView = _playerServer.GetPhotonView();
    }

    public override void Update()
    {
        if(_photonView.IsMine)
            base.Update();
    }

    public override void KeepingObject(UsePlayerObject UserObject)
    {
        base.KeepingObject(UserObject);
        if (_photonView.IsMine)
        {
            var idCube = ((UsePlayerObjectServer)UserObject).GetPhotonView().ViewID;
            _playerServer.GetPhotonView().RPC(nameof(_playerServer.HoldObject), RpcTarget.Others, idCube);
            print("hold");
        }
    }

    public override void Throw()
    {
        base.Throw();
        if (_photonView.IsMine)
        {
            _playerServer.GetPhotonView().RPC(nameof(_playerServer.PutObject), RpcTarget.Others);
            print("put");
        }
    }
}
