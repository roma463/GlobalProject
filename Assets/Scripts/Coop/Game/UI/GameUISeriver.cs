using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUISeriver : GameUi
{
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private GameStateServer _gameState;
    private PlayerServer _server;

    public override void Start()
    {
        base.Start();
        _server = PlayerServer.Instance;
    }

    public override void NextLevel()
    {
        _gameState.LoadSceneRPC(GetIndexCurrentScene() + 1);
    }

    public override void Menu()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        //base.OnLeftRoom();
        SceneManager.LoadScene("Menu");
    }

    private void LoadSceneRPC(int idScene)
    {
        _server.GetPhotonView().RPC(nameof(_server.LoadLevel), RpcTarget.All, idScene);
    }
}
