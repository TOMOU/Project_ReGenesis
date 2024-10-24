using System.Collections.Generic;
using System.Collections.ObjectModel;

// ==================================================
// 자동 생성된 코드입니다. 임의로 수정하지 마세요.
// ==================================================
public class CharacterStatusData
{
	/// <summary>
	/// 캐릭터 고유 인덱스
	/// </summary>
	public int Index;
	/// <summary>
	/// 직업 타입 (탱커, 딜러, 서포터)
	/// </summary>
	public ReGenesis.Enums.Character.JobType JobType;
	/// <summary>
	/// 유닛의 배치 위치
	/// </summary>
	public ReGenesis.Enums.Character.FormationType FormationType;
	/// <summary>
	/// 유닛의 일반 공격타입 (근접/원거리)
	/// </summary>
	public ReGenesis.Enums.Character.AttackType AttackType;
	/// <summary>
	/// 유닛의 공격 사거리
	/// </summary>
	public float AttackRange;
	/// <summary>
	/// 유닛의 공격 쿨타임
	/// </summary>
	public float AttackRate;
	/// <summary>
	/// 기본 체력
	/// </summary>
	public float HP;
	/// <summary>
	/// 레벨당 체력
	/// </summary>
	public float HP_Level;
	/// <summary>
	/// 등급당 체력
	/// </summary>
	public float HP_Grade;
	/// <summary>
	/// 기본 공격력
	/// </summary>
	public float ATK;
	/// <summary>
	/// 레벨당 공격력
	/// </summary>
	public float ATK_Level;
	/// <summary>
	/// 등급당 공격력
	/// </summary>
	public float ATK_Grade;
	/// <summary>
	/// 기본 방어력
	/// </summary>
	public float DEF;
	/// <summary>
	/// 레벨당 방어력
	/// </summary>
	public float DEF_Level;
	/// <summary>
	/// 등급당 방어력
	/// </summary>
	public float DEF_Grade;
	/// <summary>
	/// 기본 마법 방어력
	/// </summary>
	public float MAG;
	/// <summary>
	/// 레벨당 마법 방어력
	/// </summary>
	public float MAG_Level;
	/// <summary>
	/// 등급당 마법 방어력
	/// </summary>
	public float MAG_Grade;
	/// <summary>
	/// 이동 속도
	/// </summary>
	public float SPD;
	/// <summary>
	/// 치명타 확률
	/// </summary>
	public float CRIT;
	/// <summary>
	/// 치명타 데미지 배율
	/// </summary>
	public float CRIT_DMG;
	/// <summary>
	/// 회피율
	/// </summary>
	public float DODGE;

	public CharacterStatusData(int index, ReGenesis.Enums.Character.JobType jobtype, ReGenesis.Enums.Character.FormationType formationtype, ReGenesis.Enums.Character.AttackType attacktype, float attackrange, float attackrate, float hp, float hp_level, float hp_grade, float atk, float atk_level, float atk_grade, float def, float def_level, float def_grade, float mag, float mag_level, float mag_grade, float spd, float crit, float crit_dmg, float dodge)
	{
		Index = index;
		JobType = jobtype;
		FormationType = formationtype;
		AttackType = attacktype;
		AttackRange = attackrange;
		AttackRate = attackrate;
		HP = hp;
		HP_Level = hp_level;
		HP_Grade = hp_grade;
		ATK = atk;
		ATK_Level = atk_level;
		ATK_Grade = atk_grade;
		DEF = def;
		DEF_Level = def_level;
		DEF_Grade = def_grade;
		MAG = mag;
		MAG_Level = mag_level;
		MAG_Grade = mag_grade;
		SPD = spd;
		CRIT = crit;
		CRIT_DMG = crit_dmg;
		DODGE = dodge;
	}
}

public class CharacterStatusTable : ITable
{
	private List<CharacterStatusData> _table;
	public ReadOnlyCollection<CharacterStatusData> Table => _table.AsReadOnly();

	public void Load()
	{
		string className = GetType().Name;
		CSVReader reader = CSVReader.Load(className);
		if (reader == null)
		{
			Logger.LogErrorFormat("Failed to load {0}.", className);
			return;
		}

		_table = new List<CharacterStatusData>();

		for (int i = 3; i < reader.rowCount; i++)
		{
			var row = reader.GetRow(i);
			if (row != null)
			{
				int index = row.GetValue<int>(0);
				ReGenesis.Enums.Character.JobType jobtype = row.GetValue<ReGenesis.Enums.Character.JobType>(1);
				ReGenesis.Enums.Character.FormationType formationtype = row.GetValue<ReGenesis.Enums.Character.FormationType>(2);
				ReGenesis.Enums.Character.AttackType attacktype = row.GetValue<ReGenesis.Enums.Character.AttackType>(3);
				float attackrange = row.GetValue<float>(4);
				float attackrate = row.GetValue<float>(5);
				float hp = row.GetValue<float>(6);
				float hp_level = row.GetValue<float>(7);
				float hp_grade = row.GetValue<float>(8);
				float atk = row.GetValue<float>(9);
				float atk_level = row.GetValue<float>(10);
				float atk_grade = row.GetValue<float>(11);
				float def = row.GetValue<float>(12);
				float def_level = row.GetValue<float>(13);
				float def_grade = row.GetValue<float>(14);
				float mag = row.GetValue<float>(15);
				float mag_level = row.GetValue<float>(16);
				float mag_grade = row.GetValue<float>(17);
				float spd = row.GetValue<float>(18);
				float crit = row.GetValue<float>(19);
				float crit_dmg = row.GetValue<float>(20);
				float dodge = row.GetValue<float>(21);

				CharacterStatusData data = new CharacterStatusData(index, jobtype, formationtype, attacktype, attackrange, attackrate, hp, hp_level, hp_grade, atk, atk_level, atk_grade, def, def_level, def_grade, mag, mag_level, mag_grade, spd, crit, crit_dmg, dodge);
				_table.Add(data);
			}
		}
	}
}
