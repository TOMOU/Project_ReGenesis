public class Scene_Title : SceneBase
{
    public override void Initialize()
    {
        base.Initialize();

        SoundManager.Instance.PlayBGM("Sound/BGM/Eternal Light");
        UIManager.Instance.Open<Panel_Title>(false);
    }

    public override void Release()
    {
        base.Release();
    }
}
