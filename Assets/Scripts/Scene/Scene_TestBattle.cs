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
        // 캐릭터 5마리 배치
        for (int i = 100000; i < 100005; i++)
        {
            CreateCharacterEntity(i);
        }
    }

    private void CreateCharacterEntity(int index)
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
                    obj.transform.localPosition = Vector3.zero;
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
