using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ServerSinglton : Singlton
{
    [SerializeField] protected PhotonView _photonView;

    public override void Awake()
    {
        if (_photonView.IsMine)
        {
            base.Awake();
        }
    }
}
