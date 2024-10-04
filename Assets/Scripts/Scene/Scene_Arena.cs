public class Scene_Arena : SceneBase
{
    public override void Initialize()
    {
        base.Initialize();

        SoundManager.Instance.PlayBGM(12);
        UIManager.Instance.Open<Panel_Arena>();
    }

    public override void Release()
    {
        base.Release();
    }
}
