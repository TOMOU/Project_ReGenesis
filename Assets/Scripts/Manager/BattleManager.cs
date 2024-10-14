using System.Collections.Generic;

public class BattleManager : MonoSingleton<BattleManager>
{
    private List<ReGenesis.Info.Character> _characterList;

    protected override void Init()
    {
        base.Init();

        _characterList = new List<ReGenesis.Info.Character>();
    }

    protected override void Release()
    {
        base.Release();

        _characterList?.Clear();
        _characterList = null;
    }

    /// <summary>
    /// 현재 전투에 캐릭터를 추가한다.
    /// </summary>
    /// <param name="character">추가할 캐릭터 Info</param>
    /// <param name="refresh">해당 캐릭터를 바로 Spawn 할것인지</param>
    public void AddCharacter(ReGenesis.Info.Character character, bool refresh = false)
    {
        if (character == null)
        {
            Logger.LogError("Character is null.");
            return;
        }

        _characterList.Add(character);
    }

    public List<ReGenesis.Info.Character> GetCharacter()
    {
        return _characterList;
    }
}
