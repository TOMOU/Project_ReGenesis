using ReGenesis.Enums.Character;
using System.Linq;
using UnityEngine;

public class CharacterEntity : MonoBehaviour
{
    /// <summary>
    /// 캐릭터의 정보
    /// </summary>
    private Info.Character _my;
    /// <summary>
    /// 캐릭터의 FSM 로직
    /// </summary>
    private FSM.CharacterBattleFSM _fsm;
    /// <summary>
    /// 공격 대상이 되는 타겟 캐릭터
    /// </summary>
    private CharacterEntity _target;
    /// <summary>
    /// 공격 사거리 (status 테이블의 AttackRange의 제곱값).
    /// <br>이 사거리 이내면 공격루틴으로 들어간다.</br>
    /// </summary>
    private float _attackRange;
    /// <summary>
    /// 현재 공격 쿨타임.
    /// <br>해당값이 status 테이블의 AttackRate 이상이 되면 공격을 시작한다.</br>
    /// </summary>
    private float _attackRate;
    /// <summary>
    /// 강제 쿨타임 적용
    /// </summary>
    private bool _isForceAttackRate;

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
                _my = new Info.Character(index, grade, level, data);

                float range = _my.status.AttackRange;
                _attackRange = range * range;
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
        _fsm.cbAttack = OnAttack;
        _fsm.cbSkill_0 = OnSkill_0;
        _fsm.cbSkill_1 = OnSkill_1;
        _fsm.cbSkill_2 = OnSkill_2;
        _fsm.cbStun = OnStun;
        _fsm.cbVictory = OnVictory;
        _fsm.cbDie = OnDie;
    }

    /// <summary>
    /// 캐릭터가 살아있는지를 체크
    /// </summary>
    /// <returns>true: 살아있다</returns>
    public bool IsAlive()
    {
        return _my.status.HP > 0;
    }

    /// <summary>
    /// 타겟 캐릭터를 찾는다.
    /// </summary>
    /// <returns>true: 검색완료, 타겟 캐릭터가 유효하다.</returns>
    private bool CheckTarget()
    {
        // 타겟이 죽었거나 없다.
        if (_target == null)
        {
            _target = BattleManager.Instance.FindCharacter(this);
            if (_target == null)
            {
                // 모든 적이 죽었다. Victory 상태로는 OnIdle에서 동작시킨다.
                return false;
            }
        }

        // 타겟이 살아있는지 체크. 죽었다면 Idle 후 재탐색.
        if (_target.IsAlive() == false)
        {
            _target = null;
            return false;
        }

        return true;
    }

    /// <summary>
    /// 타겟 캐릭터와의 거리를 체크해 사거리 이내인지 확인한다.
    /// </summary>
    /// <returns>true: 공격 사거리 이내이다.</returns>
    private bool CheckDistance()
    {
        float distance = transform.GetDistance(_target.transform);

        // 사거리가 되지 않는다.
        if (distance > _attackRange)
        {
            return false;
        }

        // 사거리가 되니 대기상태 넘어간다.
        return true;
    }

    /// <summary>
    /// 공격 쿨타임이 찼는지 체크한다.
    /// </summary>
    /// <returns>true: 공격 시작 가능</returns>
    private bool CheckAttackRate()
    {
        if (_attackRate >= _my.status.AttackRate)
        {
            _attackRate = 0;
            return true;
        }

        _attackRate += Time.deltaTime;
        return false;
    }

    public void SendDamage(Info.Character sender)
    {
        // 데미지 계산 (기본 데미지의 90~110%)
        float dmg = sender.status.ATK;
        float rnd = Random.Range(0.9f, 1.1f);
        dmg = dmg * rnd;

        // 크리티컬확률 계산
        float activeCritical = Random.Range(0f, 1f + Mathf.Epsilon);
        if (sender.status.CRIT <= activeCritical)
        {
            dmg = dmg * sender.status.CRIT_DMG;
        }

        // 데미지의 최종 확정 (float값의 소숫점을 올림)
        int finalDamage = Mathf.CeilToInt(dmg);

        int prevHP = _my.status.HP;

        // 데미지를 적용
        _my.status.HP -= finalDamage;
        Logger.LogFormat("[{0}] {1} → {2}", gameObject.name, prevHP, _my.status.HP);
        if (_my.status.HP <= 0)
        {
            _fsm.ChangeState(StateFSM.Die);
        }
    }

    #region FSM Callback
    /// <summary>
    /// 대기 중 Update 시 동작.
    /// </summary>
    private void OnIdle()
    {
        // 공격모션 중 적이 죽었다면 강제 쿨타임 적용.
        if (_isForceAttackRate == true)
        {
            if (CheckAttackRate() == true)
            {
                _isForceAttackRate = false;
            }

            return;
        }

        // 타겟이 살아있는지 체크
        if (CheckTarget() == false)
        {
            _fsm.ChangeState(StateFSM.Victory);
            return;
        }

        // 사거리 체크
        if (CheckDistance() == false)
        {
            _fsm.ChangeState(StateFSM.Run);
            return;
        }

        // 공격 쿨타임 체크
        if (CheckAttackRate() == false)
        {
            return;
        }

        // 스킬 쿨타임 체크
        // TODO

        // 공격
        _fsm.ChangeState(StateFSM.Attack);
    }

    /// <summary>
    /// 이동 중 Update 시 동작.
    /// </summary>
    private void OnRun()
    {
        // 타겟이 살아있는지 체크
        if (CheckTarget() == false)
        {
            return;
        }

        // 사거리 체크. 사거리 밖이면 이동을 계속.
        if (CheckDistance() == false)
        {
            transform.MoveTo(_target.transform, _my.status.SPD, _fsm.skeleton);
            return;
        }

        // 사거리가 되었으니 Idle로 이행하여 공격쿨타임을 대기한다.
        _fsm.ChangeState(StateFSM.Idle);
    }

    private void OnAttack()
    {
        // 공격 모션이 끝났는지 확인
        if (_fsm.skeleton.AnimationState.GetCurrent(0).IsComplete == true)
        {
            // TEST: 임시 데미지 적립
            _target.SendDamage(_my);

            if (_target.IsAlive() == false)
            {
                _target = null;
                _isForceAttackRate = true;
            }

            _attackRate = 0;
            _fsm.ChangeState(StateFSM.Idle, FSM.StateTransition.Overwrite);
        }
    }

    private void OnSkill_0()
    {

    }

    private void OnSkill_1()
    {

    }

    private void OnSkill_2()
    {

    }

    private void OnStun()
    {

    }

    private void OnVictory()
    {

    }

    private void OnDie()
    {

    }
    #endregion
}
