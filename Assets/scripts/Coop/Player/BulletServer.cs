using Photon.Pun;
using UnityEngine;

public class BulletServer : Bullet
{
    [SerializeField] private PhotonView _photonView;
    private int _idMinePlayer;

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    [PunRPC]
    public void GetPlayerId(int id)
    {
        if (!_photonView.IsMine)
        {
            var player = PhotonView.Find(id);
            print(id);
            _teleport = player.GetComponent<Teleport>();
        }
    }

    public void GetIdPlayerPhoton(int idPlayer)
    {
        _idMinePlayer = idPlayer;
        _photonView.RPC(nameof(GetPlayerId), RpcTarget.Others, idPlayer);
    }

    public override void TeleportPlayer(Teleport.Offset normal, Vector2 positionCollision)
    {
        base.TeleportPlayer(normal, positionCollision);
    }

    public override void DestroyBullet()
    {
        PhotonNetwork.Destroy(_photonView);
    }
}
