using UnityEngine;

public class Scene_TestBattle : SceneBase
{
    public override void Initialize()
    {
        base.Initialize();

        GameObject obj = new GameObject("TestScript");
        obj.AddComponent<TestScript>();
    }

    public override void Release()
    {
        base.Release();
    }
}
