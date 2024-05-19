using Photon.Pun;
using UnityEngine;

public class UndependBlockServer : UndependBlock
{
    [SerializeField] private PhotonView _photonView;

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if(_photonView.IsMine)
            base.OnCollisionEnter2D(collision);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if(_photonView.IsMine)
            base.OnTriggerEnter2D(collision);
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if(_photonView.IsMine)
            base.OnTriggerExit2D(collision);
    }
}
