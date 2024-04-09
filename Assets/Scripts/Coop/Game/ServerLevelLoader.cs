using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerLevelLoader : MonoBehaviour
{
    [SerializeField] private PhotonView _photonView;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartPhoton();
        }
    }
    public void RestartPhoton()
    {
        PhotonNetwork.LoadLevel(1);
        _photonView.RPC(nameof(LoadSceneRPC), RpcTarget.All, 1);
        print("Server restart");

    }


    [PunRPC]
    public void LoadSceneRPC(int idScene)
    {
        // Загрузка новой сцены на всех клиентах
        //if (!_photonView.IsMine)
        //    return;
        PhotonNetwork.LoadLevel(idScene);
    }
}
