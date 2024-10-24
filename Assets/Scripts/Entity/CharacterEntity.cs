using ReGenesis.Enums.Character;
using System.Linq;
using UnityEngine;

public class CharacterEntity : MonoBehaviour
{
    // 캐릭터의 정보
    public Info.Character character;
    // 캐릭터의 전투 로직
    private FSM.CharacterBattleFSM _fsm;

    private CharacterEntity _target;

    /// <summary>
    /// index, grade, level정보로 생성한 캐릭터를 초기화 (추후 고유번호를 통한 캐릭터 생성으로 전환)
    /// </summary>
    /// <param name="index">캐릭터의 인덱스</param>
    /// <param name="grade">캐릭터의 등급</param>
    /// <param name="level">캐릭터의 레벨</param>
    public void Initialize(int index, int grade, int level)
    {
        Init_Status(index, grade, level);
        Init_FSM();
    }

    private void Init_Status(int index, int grade, int level)
    {
        CharacterStatusTable table = TableManager.Instance.GetTable<CharacterStatusTable>();
        if (table != null)
        {
            CharacterStatusData data = table.Table.FirstOrDefault(e => e.Index == index);
            if (data != null)
            {
                character = new Info.Character(index, grade, level, data);
            }
        }
    }

    private void Init_FSM()
    {
        if (_fsm == null)
            _fsm = gameObject.AddComponent<FSM.CharacterBattleFSM>();
        _fsm.Initialize();

        _fsm.cbIdle = OnIdle;
        _fsm.cbRun = OnRun;
    }

    private bool CheckTarget()
    {
        // 타겟이 죽었거나 없다.
        if (_target == null)
        {
            _target = BattleManager.Instance.FindCharacter(this);
            if (_target == null)
            {
                return false;
            }
        }

        // 타겟이 살아있는지 체크
        // blah blah~~

        return true;
    }

    private bool CheckDistance()
    {
        float distance = BattleCalc.GetDistance(transform, _target.transform);

        // 사거리가 되지 않는다.
        if (distance > 5 * 5)
        {
            return false;
        }

        // 사거리가 되니 대기상태 넘어간다.
        return true;
    }

    private void OnIdle()
    {
        if (CheckTarget() == false)
        {
            return;
        }

        if (CheckDistance() == false)
        {
            _fsm.ChangeState(StateFSM.Run);
            return;
        }

        //_fsm.ChangeState(StateFSM.Idle);
    }

    private void OnRun()
    {
        if (CheckTarget() == false)
        {
            return;
        }

        if (CheckDistance() == false)
        {
            BattleCalc.MoveTo(transform, _target.transform, 4f, _fsm.skeleton);
            //_fsm.ChangeState(StateFSM.Run);
            return;
        }

        _fsm.ChangeState(StateFSM.Idle);
    }
}
