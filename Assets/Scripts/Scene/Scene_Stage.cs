public class Scene_Stage : SceneBase
{
    public override void Initialize()
    {
        SoundManager.Instance.PlayBGM(11);
    }

    public override void Release()
    {
        base.Release();
    }
}
