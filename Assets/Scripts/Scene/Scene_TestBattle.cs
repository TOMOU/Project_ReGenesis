public class Scene_TestBattle : SceneBase
{
    public override void Initialize()
    {
        base.Initialize();

        SoundManager.Instance.PlayBGM(11);

        TestFunction();
    }

    public override void Release()
    {
        base.Release();
    }

    private void TestFunction()
    {

    }
}
