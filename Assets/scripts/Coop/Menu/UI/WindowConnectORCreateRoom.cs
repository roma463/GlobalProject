using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowConnectORCreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _idRoom;
    [SerializeField] private WindowError _windowError;

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(_idRoom.text);
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(CreateIdRoom(), new Photon.Realtime.RoomOptions{ MaxPlayers = 2});
    }

    private string CreateIdRoom()
    {
        string id = "";
        for (int i = 0; i < 4; i++)
        {
            id += Random.Range(0, 9);
        }
        return id;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _windowError.Active("комнаты с таким id не существует");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }

}
