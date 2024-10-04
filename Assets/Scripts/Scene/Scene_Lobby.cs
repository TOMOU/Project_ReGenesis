public class Scene_Lobby : SceneBase
{
    public override void Initialize()
    {
        base.Initialize();

        SoundManager.Instance.PlayBGM(15);
        UIManager.Instance.Open<Panel_Lobby>();
    }

    public override void Release()
    {
        base.Release();
    }
}
