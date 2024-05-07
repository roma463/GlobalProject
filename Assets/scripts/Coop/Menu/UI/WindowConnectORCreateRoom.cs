using TMPro;
using UnityEngine;

public class WindowConnectORCreateRoom : WindowUI
{
    [SerializeField] private TMP_InputField _idRoom;
    [SerializeField] private TextMeshProUGUI _errorNullId;
    [SerializeField] private ConnectionToServer _server;

    public void JoinRoom()
    {
        if (_idRoom.text.Length == 0)
        {
            _errorNullId.gameObject.SetActive(true);
            return;
        }
        _server.JoinRoom(_idRoom.text);
    }

    public void CreateRoom()
    {
        _server.CreateRoom();
    }
}
