using Photon.Pun;
using System.Collections;
using UnityEngine;

public class PositionGunServer : PositionGun, IPunObservable
{
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private float _forceSmooth = 1;
    private Vector2 _targetVelosity;


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

    public override Bullet CreateBullet(Vector2 position)
    {
        var prefab = GetBullet();
        var bullet = PhotonNetwork.Instantiate(prefab.name, position, Quaternion.identity).GetComponent<Bullet>();
        ((BulletServer)bullet).GetIdPlayerPhoton(_photonView.ViewID);
        return bullet;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        var timeVelosity = new Vector2();
        if (stream.IsWriting)
        {
            timeVelosity = Velosity;
            stream.SendNext(timeVelosity);
        }
        else
        {
            timeVelosity = (Vector2) stream.ReceiveNext();
            _targetVelosity = timeVelosity;
        }
    }

    private IEnumerator SmoothChangeVelosityOnClonePlayer()
    {
        while(true)
        {
            Velosity = Vector2.Lerp(Velosity, _targetVelosity, _forceSmooth);
            yield return null;
        }    
    }
}
