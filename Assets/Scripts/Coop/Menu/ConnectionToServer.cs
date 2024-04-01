using System.Collections;
using UnityEngine;
using Photon.Pun;

public class ConnectionToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private UIServer _ui;

    private void Start()
    {
        PhotonNetwork.NickName = "Player";
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }


    public IEnumerator Connection()
    {
        yield return new WaitUntil(GetConnectionToServer);
        print("IsConnetcion");
    }

    private bool GetConnectionToServer()
    {
        return PhotonNetwork.IsConnected;
    }

    public IEnumerator Disconection(System.Action actionEvent)
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
        {
            yield return null;
        }
        PhotonNetwork.OfflineMode = true;
        actionEvent?.Invoke();
    }

    public override void OnConnectedToMaster()
    {
        _ui.ActiveCoopButton();
    }

}
