using Photon.Pun;
using UnityEngine;

public class BulletServer : Bullet
{
    [SerializeField] private PhotonView _photonView;
    private int _idMinePlayer;

    public override void CollisionLine(Vector2 origin, Vector2 ending)
    {
        base.CollisionLine(origin, ending);
    }

    public override void CheckCollisionSurface(RaycastHit2D hit)
    {
        if (_photonView.IsMine)
            base.CheckCollisionSurface(hit);
        else
        {
            DestroyBullet();
        }    
    }

    [PunRPC]
    public void SendingData(int id, Vector2 startSpeed, int gravityScale)
    {
        if (!_photonView.IsMine)
        {
            var player = PhotonView.Find(id);
            _teleport = player.GetComponent<Teleport>();
        }
        _moveBullet.Initialize(startSpeed, gravityScale);
    }

    public void ServerParamentrsInit(int idPlayer, Vector2 StartSpeed, int garavityScale)
    {
        _idMinePlayer = idPlayer;
        var timeCreate = PhotonNetwork.Time;
        _photonView.RPC(nameof(SendingData), RpcTarget.Others, idPlayer, StartSpeed, garavityScale);
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
}
