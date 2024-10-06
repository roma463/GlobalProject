using Photon.Pun;

public class GameStateServer : GameState
{
    private PlayerServer _server;

    public override void Start()
    {
        base.Start();
        _server = PlayerServer.Instance;
    }

    public override void Update()
    {
        if(PlayerButton != null)
            base.Update();
    }

    public override void Restart()
    {
        LoadSceneRPC(CurrentScene);
    }

    public override void SaveLevel()
    {
        _save.Saves.LevelJointsIndex++;
        _save.SaveData();
    }

    public override void LoadNextLevel()
    {
        var indexLevel = _levelList.GetCoopNextIndex(_save.Saves.LevelJointsIndex);
        LoadSceneRPC(indexLevel);
    }

    public void LoadSceneRPC(int idScene)
    {
        _server.GetPhotonView().RPC(nameof(_server.LoadLevel), RpcTarget.All, idScene);
    }
}
