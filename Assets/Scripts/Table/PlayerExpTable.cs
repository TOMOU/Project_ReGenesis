using System.Collections.Generic;
using System.Collections.ObjectModel;

// ==================================================
// 자동 생성된 코드입니다. 임의로 수정하지 마세요.
// ==================================================
public class PlayerExpData
{
	/// <summary>
	/// 플레이어의 현재 레벨
	/// </summary>
	public int Level_Current;
	/// <summary>
	/// 플레이어의 다음 레벨
	/// </summary>
	public int Level_Next;
	/// <summary>
	/// 현재 레벨에서 필요한 경험치
	/// </summary>
	public int EXP;
	/// <summary>
	/// 누적된 경험치
	/// </summary>
	public int EXP_Total;

	public PlayerExpData(int level_current, int level_next, int exp, int exp_total)
	{
		Level_Current = level_current;
		Level_Next = level_next;
		EXP = exp;
		EXP_Total = exp_total;
	}
}

public class PlayerExpTable : ITable
{
	private List<PlayerExpData> _table;
	public ReadOnlyCollection<PlayerExpData> Table => _table.AsReadOnly();

	public void Load()
	{
		string className = GetType().Name;
		CSVReader reader = CSVReader.Load(className);
		if (reader == null)
		{
			Logger.LogErrorFormat("Failed to load {0}.", className);
			return;
		}

		_table = new List<PlayerExpData>();

		for (int i = 3; i < reader.rowCount; i++)
		{
			var row = reader.GetRow(i);
			if (row != null)
			{
				int level_current = row.GetValue<int>(0);
				int level_next = row.GetValue<int>(1);
				int exp = row.GetValue<int>(2);
				int exp_total = row.GetValue<int>(3);

				PlayerExpData data = new PlayerExpData(level_current, level_next, exp, exp_total);
				_table.Add(data);
			}
		}
	}
}
