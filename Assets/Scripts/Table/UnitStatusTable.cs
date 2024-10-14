using System.Collections.Generic;
using System.Collections.ObjectModel;

// ==================================================
// 자동 생성된 코드입니다. 임의로 수정하지 마세요.
// ==================================================
public class UnitStatusData
{
	/// <summary>
	/// 유닛 고유 인덱스
	/// </summary>
	public int Index;
	/// <summary>
	/// 유닛 이름
	/// </summary>
	public string Name;
	/// <summary>
	/// 직업 타입 (탱커, 딜러, 서포터)
	/// </summary>
	public Constant.UnitJobType Job;
	/// <summary>
	/// 유닛의 배치 위치
	/// </summary>
	public Constant.UnitPosition Position;
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

	public UnitStatusData(int index, string name, Constant.UnitJobType job, Constant.UnitPosition position, float hp, float hp_level, float hp_grade, float atk, float atk_level, float atk_grade, float def, float def_level, float def_grade, float mag, float mag_level, float mag_grade, float spd, float crit, float crit_dmg)
	{
		Index = index;
		Name = name;
		Job = job;
		Position = position;
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
	}
}

public class UnitStatusTable : ITable
{
	private List<UnitStatusData> _table;
	public ReadOnlyCollection<UnitStatusData> Table => _table.AsReadOnly();

	public void Load()
	{
		string className = GetType().Name;
		CSVReader reader = CSVReader.Load(className);
		if (reader == null)
		{
			Logger.LogErrorFormat("Failed to load {0}.", className);
			return;
		}

		_table = new List<UnitStatusData>();

		for (int i = 3; i < reader.rowCount; i++)
		{
			var row = reader.GetRow(i);
			if (row != null)
			{
				int index = row.GetValue<int>(0);
				string name = row.GetValue<string>(1);
				Constant.UnitJobType job = row.GetValue<Constant.UnitJobType>(2);
				Constant.UnitPosition position = row.GetValue<Constant.UnitPosition>(3);
				float hp = row.GetValue<float>(4);
				float hp_level = row.GetValue<float>(5);
				float hp_grade = row.GetValue<float>(6);
				float atk = row.GetValue<float>(7);
				float atk_level = row.GetValue<float>(8);
				float atk_grade = row.GetValue<float>(9);
				float def = row.GetValue<float>(10);
				float def_level = row.GetValue<float>(11);
				float def_grade = row.GetValue<float>(12);
				float mag = row.GetValue<float>(13);
				float mag_level = row.GetValue<float>(14);
				float mag_grade = row.GetValue<float>(15);
				float spd = row.GetValue<float>(16);
				float crit = row.GetValue<float>(17);
				float crit_dmg = row.GetValue<float>(18);

				UnitStatusData data = new UnitStatusData(index, name, job, position, hp, hp_level, hp_grade, atk, atk_level, atk_grade, def, def_level, def_grade, mag, mag_level, mag_grade, spd, crit, crit_dmg);
				_table.Add(data);
			}
		}
	}
}
