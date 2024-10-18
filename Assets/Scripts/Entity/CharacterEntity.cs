using System.Linq;
using UnityEngine;

public class CharacterEntity : MonoBehaviour
{
    public int index;
    public int grade;
    public int level;

    // 캐릭터의 스테이터스
    public Info.Status status;
    // 캐릭터의 장비정보
    // [~~~~~~~~~~~~~~~~~~~~]
    // 캐릭터의 전투 로직
    private FSM.CharacterBattleFSM _fsm;

    /// <summary>
    /// index, grade, level정보로 생성한 캐릭터를 초기화 (추후 고유번호를 통한 캐릭터 생성으로 전환)
    /// </summary>
    /// <param name="index">캐릭터의 인덱스</param>
    /// <param name="grade">캐릭터의 등급</param>
    /// <param name="level">캐릭터의 레벨</param>
    public void Initialize(int index, int grade, int level)
    {
        this.index = index;
        this.grade = grade;
        this.level = level;

        Init_FSM();
        Init_Status();
    }

    private void Init_Status()
    {
        CharacterStatusTable table = TableManager.Instance.GetTable<CharacterStatusTable>();
        if (table != null)
        {
            CharacterStatusData data = table.Table.FirstOrDefault(e => e.Index == index);
            if (data != null)
            {
                status = new Info.Status(index, grade, level, data);
            }
        }
    }

    private void Init_FSM()
    {
        if (_fsm == null)
            _fsm = gameObject.AddComponent<FSM.CharacterBattleFSM>();
        _fsm.Initialize();
    }
}
