public class Scene_Lobby : SceneBase
{
    public override void Initialize()
    {
        SoundManager.Instance.PlayBGM(15);
    }

    public override void Release()
    {
        base.Release();
    }
}
