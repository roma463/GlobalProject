using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ServerSinglton : MonoBehaviour
{
    [SerializeField] protected PhotonView _photonView;

    public virtual void Awake()
    {
        if (_photonView.IsMine)
        {
            InitializeSingleton();
        }
    }

    public abstract void InitializeSingleton();
}
