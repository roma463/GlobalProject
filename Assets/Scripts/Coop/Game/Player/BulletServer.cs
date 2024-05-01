using Photon.Pun;
using UnityEngine;

public class BulletServer : Bullet
{
    [SerializeField] private PhotonView _photonView;
    private int _idMinePlayer;

    public override void CollisionLine(Vector2 origin, Vector2 ending)
    {
        if (_photonView.IsMine)
            base.CollisionLine(origin, ending);
    }

    public override void CheckCollisionCollider(RaycastHit2D hit)
    {
        if (_photonView.IsMine)
            base.CheckCollisionCollider(hit);
        else
            base.DestroyBullet();
    }

    [PunRPC]
    public void SendingData(int id, Vector2 startSpeed, double timeCreate)
    {
        if (!_photonView.IsMine)
        {
            var player = PhotonView.Find(id);
            _teleport = player.GetComponent<Teleport>();
        }
        _moveBullet.Initialize(startSpeed, timeCreate);
    }

    public void ServerParamentrsInit(int idPlayer, Vector2 StartSpeed)
    {
        _idMinePlayer = idPlayer;
        var timeCreate = PhotonNetwork.Time;
        _photonView.RPC(nameof(SendingData), RpcTarget.Others, idPlayer, StartSpeed, timeCreate);
    }

    public override void TeleportPlayer(Teleport.Offset normal, Vector2 positionCollision)
    {
        _photonView.RPC(nameof(SyncTeleport), RpcTarget.All, normal, positionCollision);
    }

    [PunRPC]
    public void SyncTeleport(Teleport.Offset normal, Vector2 positionCollision)
    {
        base.TeleportPlayer(normal, positionCollision);
    }

    public override void DestroyBullet()
    {
        PhotonNetwork.Destroy(_photonView);
    }
}
