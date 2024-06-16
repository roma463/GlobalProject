using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectionToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private UIServer _ui;
    [SerializeField] private WindowError w_error;

    private void Start()
    {
        if (PhotonNetwork.NetworkClientState == ClientState.Disconnected || PhotonNetwork.NetworkClientState == ClientState.PeerCreated)
        {
            PhotonNetwork.NickName = "Player";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
        else if(PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer)
        {
            _ui.ActiveCoopButton();
        }
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

    public override void OnConnected()
    {
        print("местный конект");
    }

    public override void OnConnectedToMaster()
    {
        _ui.ActiveCoopButton();
    }

    #region CREATE&JOIN


    private string CreateIdRoom()
    {
        string id = "";
        for (int i = 0; i < 4; i++)
        {
            id += Random.Range(0, 9);
        }
        return id;
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(CreateIdRoom(), new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
    }

    public void JoinRoom(string idRoom)
    {
        PhotonNetwork.JoinRoom(idRoom);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        w_error.Activate();
    }

    #endregion
}
