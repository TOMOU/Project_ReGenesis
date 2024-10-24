using System.Linq;
using UnityEngine;

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
        // 아군
        CreateCharacterEntity(100000, new Vector3(-12, -3));

        // 적군
        CreateCharacterEntity(100003, new Vector3(12, -3));
    }

    private void CreateCharacterEntity(int index, Vector3 vec)
    {
        var table = TableManager.Instance.GetTable<CharacterTable>();
        if (table != null)
        {
            CharacterData data = table.Table.FirstOrDefault(e => e.Index == index);
            if (data != null)
            {
                GameObject prefab = ResourceManager.Instance.LoadCharacterSpineModel(data.SpineFolderName);
                if (prefab != null)
                {
                    GameObject obj = GameObject.Instantiate(prefab);
                    obj.name = LocalizationManager.Instance.GetString(data.Name);
                    obj.transform.localPosition = vec;
                    obj.transform.localScale = Vector3.one * 0.3f;

                    // Entity 추가
                    CharacterEntity entity = obj.AddComponent<CharacterEntity>();
                    entity.Initialize(index, 1, 1);

                    BattleManager.Instance.AddCharacter(entity);
                }
            }
        }
    }
}
