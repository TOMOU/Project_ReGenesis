public class Scene_Title : SceneBase
{
    public override void Initialize()
    {
        base.Initialize();

        SoundManager.Instance.PlayBGM(0);
        UIManager.Instance.Open<Panel_Title>();
    }

    public override void Release()
    {
        base.Release();
    }
}
