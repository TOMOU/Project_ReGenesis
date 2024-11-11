using System.Collections.Generic;
using UnityEngine;

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
            else if (c.teamType == my.teamType)
            {
                continue;
            }
            else if (c.IsAlive() == false)
            {
                continue;
            }

            float distance = my.transform.GetDistance(c.transform);
            if (distance < minDistance)
            {
                minDistance = distance;
                target = c;
            }
        }

        return target;
    }

    private void SortCharacter()
    {
        // 캐릭터의 y위치를 기반으로 내림차순 정렬
        _characterList.Sort((CharacterEntity a, CharacterEntity b) =>
        {
            return b.transform.localPosition.y.CompareTo(a.transform.localPosition.y);
        });

        // 순서대로 orderLayer를 변경
        for (int i = 0; i < _characterList.Count; i++)
        {
            if (_characterList[i].skeletonMesh == null)
            {
                _characterList[i].skeletonMesh = _characterList[i].GetComponent<MeshRenderer>();
            }

            _characterList[i].skeletonMesh.sortingOrder = 10 + i * 10;
        }
    }

    private void LateUpdate()
    {
        if (_characterList == null)
        {
            return;
        }

        SortCharacter();
    }
}
