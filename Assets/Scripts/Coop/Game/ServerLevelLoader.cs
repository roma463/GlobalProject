using Photon.Pun;
using UnityEngine;

public class ServerLevelLoader : MonoBehaviour
{
    [SerializeField] private PhotonView _photonView;

    public void RestartPhoton()
    {
        PhotonNetwork.LoadLevel(1);
        _photonView.RPC(nameof(LoadSceneRPC), RpcTarget.All, 1);
    }

    [PunRPC]
    public void LoadSceneRPC(int idScene)
    {
        PhotonNetwork.LoadLevel(idScene);
    }
}
