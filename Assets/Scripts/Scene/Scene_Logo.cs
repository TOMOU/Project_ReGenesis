public class Scene_Logo : SceneBase
{
    public override void Initialize()
    {
        base.Initialize();

        UIManager.Instance.Open<Panel_Logo>(false);
    }

    public override void Release()
    {
        base.Release();
    }
}