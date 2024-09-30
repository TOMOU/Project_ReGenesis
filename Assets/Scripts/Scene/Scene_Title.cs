public class Scene_Title : SceneBase
{
    public override void Initialize()
    {
        SoundManager.Instance.PlayBGM(0);
    }

    public override void Release()
    {
        base.Release();
    }
}
