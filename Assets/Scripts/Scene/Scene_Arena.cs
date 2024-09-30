public class Scene_Arena : SceneBase
{
    public override void Initialize()
    {
        SoundManager.Instance.PlayBGM(12);
    }

    public override void Release()
    {
        base.Release();
    }
}
