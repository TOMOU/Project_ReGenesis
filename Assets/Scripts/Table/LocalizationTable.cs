using System.Collections.Generic;
using System.Collections.ObjectModel;

// ==================================================
// 자동 생성된 코드입니다. 임의로 수정하지 마세요.
// ==================================================
public class LocalizationData
{
	/// <summary>
	/// 인덱스
	/// </summary>
	public int Index;
	/// <summary>
	/// 한글 텍스트
	/// </summary>
	public string Text_KR;
	/// <summary>
	/// 일본어 텍스트
	/// </summary>
	public string Text_JP;
	/// <summary>
	/// 영어 텍스트
	/// </summary>
	public string Text_EN;

	public LocalizationData(int index, string text_kr, string text_jp, string text_en)
	{
		Index = index;
		Text_KR = text_kr;
		Text_JP = text_jp;
		Text_EN = text_en;
	}
}

public class LocalizationTable : ITable
{
	private List<LocalizationData> _table;
	public ReadOnlyCollection<LocalizationData> Table => _table.AsReadOnly();

	public void Load()
	{
		string className = GetType().Name;
		CSVReader reader = CSVReader.Load(className);
		if (reader == null)
		{
			Logger.LogErrorFormat("Failed to load {0}.", className);
			return;
		}

		_table = new List<LocalizationData>();

		for (int i = 3; i < reader.rowCount; i++)
		{
			var row = reader.GetRow(i);
			if (row != null)
			{
				int index = row.GetValue<int>(0);
				string text_kr = row.GetValue<string>(2);
				string text_jp = row.GetValue<string>(3);
				string text_en = row.GetValue<string>(4);

				LocalizationData data = new LocalizationData(index, text_kr, text_jp, text_en);
				_table.Add(data);
			}
		}
	}
}
