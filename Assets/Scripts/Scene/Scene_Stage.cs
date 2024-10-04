public class Scene_Stage : SceneBase
{
    public override void Initialize()
    {
        base.Initialize();

        SoundManager.Instance.PlayBGM(11);
        UIManager.Instance.Open<Panel_Stage>();
    }

    public override void Release()
    {
        base.Release();
    }
}
