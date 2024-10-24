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

    public CharacterEntity FindCharacter(CharacterEntity my)
    {
        CharacterEntity target = null;
        float minDistance = float.MaxValue;

        foreach (CharacterEntity c in _characterList)
        {
            if (c == my)
            {
                continue;
            }

            float distance = BattleCalc.GetDistance(my.transform, c.transform);
            if (distance < minDistance)
            {
                minDistance = distance;
                target = c;
            }
        }

        return target;
    }
}
