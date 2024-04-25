using Photon.Pun;
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
