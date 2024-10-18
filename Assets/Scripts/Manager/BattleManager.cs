using System.Collections.Generic;

public class BattleManager : MonoSingleton<BattleManager>
{
    private List<CharacterEntity> _characterList;

    protected override void Init()
    {
        base.Init();

        _characterList = new List<CharacterEntity>();
    }

    protected override void Release()
    {
        base.Release();

        _characterList?.Clear();
        _characterList = null;
    }

    public void AddCharacter(CharacterEntity character)
    {
        if (character != null)
        {
            _characterList.Add(character);
        }
    }
}
