using Photon.Pun;
using System.Collections;
using UnityEngine;

public class PositionGunServer : PositionGun, IPunObservable
{
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private PlayerServer _playerServer;
    [SerializeField] private float _forceSmooth = 1;
    private Vector2 _targetVelosity;

    #region MONO_BEHAVIOR

    public override void Start()
    {
        if(!_photonView.IsMine)
            StartCoroutine(SmoothChangeVelosityOnClonePlayer());
        base.Start();
    }

    public override void Update()
    {
        if (_photonView.IsMine)
        {
            base.Update();
        }
        else
        {
            CreateTranjectory(Velosity);
        }
    }
    #endregion

    public override Bullet CreateBullet(Vector2 position)
    {
        var prefab = GetBullet();
        var bullet = PhotonNetwork.Instantiate(prefab.name, position, Quaternion.identity).GetComponent<Bullet>();
        ((BulletServer)bullet).ServerParamentrsInit(_photonView.ViewID, Velosity);
        return bullet;
    }

    public override void Shot()
    {
        base.Shot();
        _photonView.RPC(nameof(_playerServer.ShotPlayer), RpcTarget.Others);
    }

    public void ShotRemotePlayer()
    {
        _countShot--;
        ShotFX();
    }

    #region INTERFACE

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        var timeVelosity = new Vector2();
        if (stream.IsWriting)
        {
            timeVelosity = Velosity;
            stream.SendNext(timeVelosity);
            //stream.SendNext(_countShot);
        }
        else
        {
            timeVelosity = (Vector2) stream.ReceiveNext();
            //_countShot = (int)stream.ReceiveNext();
            _targetVelosity = timeVelosity;
        }
    }
    #endregion

    private IEnumerator SmoothChangeVelosityOnClonePlayer()
    {
        while(true)
        {
            Velosity = Vector2.Lerp(Velosity, _targetVelosity, _forceSmooth);
            yield return null;
        }    
    }
}
