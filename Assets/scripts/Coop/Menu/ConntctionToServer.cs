using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ConntctionToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private UIServer _ui;

    private void Start()
    {
        PhotonNetwork.NickName = "Player";
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        _ui.ActiveCoopButton();
        print(PhotonNetwork.CloudRegion);
    }

}
