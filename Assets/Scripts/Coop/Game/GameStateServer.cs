using Photon.Pun;

public class GameStateServer : GameState
{
    private PlayerServer _server;

    public override void Start()
    {
        base.Start();
        _server = PlayerServer.Instance;
    }

    public override void Restart()
    {
        LoadSceneRPC(CurrentScene);
    }

    public void LoadSceneRPC(int idScene)
    {
        _server.GetPhotonView().RPC(nameof(_server.LoadLevel), RpcTarget.All, idScene);
    }
}
