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
        CreateCharacterEntity(ReGenesis.Enums.Character.TeamType.Blue, 100002, new Vector3(-5, -3));
        CreateCharacterEntity(ReGenesis.Enums.Character.TeamType.Blue, 100001, new Vector3(-5, -1));
        CreateCharacterEntity(ReGenesis.Enums.Character.TeamType.Blue, 100003, new Vector3(-9, -3));
        CreateCharacterEntity(ReGenesis.Enums.Character.TeamType.Blue, 100000, new Vector3(-13, -1));
        CreateCharacterEntity(ReGenesis.Enums.Character.TeamType.Blue, 100004, new Vector3(-13, -5));

        // 적군
        CreateCharacterEntity(ReGenesis.Enums.Character.TeamType.Red, 100002, new Vector3(5, -1));
        CreateCharacterEntity(ReGenesis.Enums.Character.TeamType.Red, 100001, new Vector3(5, -5));
        CreateCharacterEntity(ReGenesis.Enums.Character.TeamType.Red, 100003, new Vector3(9, -5));
        CreateCharacterEntity(ReGenesis.Enums.Character.TeamType.Red, 100000, new Vector3(13, -3));
        CreateCharacterEntity(ReGenesis.Enums.Character.TeamType.Red, 100004, new Vector3(13, -1));
    }

    private void CreateCharacterEntity(ReGenesis.Enums.Character.TeamType teamType, int index, Vector3 vec)
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
                    entity.Initialize(teamType, index, 1, Random.Range(1, 5));

                    BattleManager.Instance.AddCharacter(entity);
                }
            }
        }
    }
}
