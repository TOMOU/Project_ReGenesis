using System.Collections.Generic;
using System.Collections.ObjectModel;

// ==================================================
// 자동 생성된 코드입니다. 임의로 수정하지 마세요.
// ==================================================
public class SoundData
{
	/// <summary>
	/// 사운드 고유 인덱스
	/// </summary>
	public int Index;
	/// <summary>
	/// 사운드 타입 (BGM, SFX, Voice 등)
	/// </summary>
	public Constant.SoundType Type;
	/// <summary>
	/// 사운드 이름
	/// </summary>
	public string Name;
	/// <summary>
	/// 사운드 파일 이름 또는 경로
	/// </summary>
	public string FilePath;
	/// <summary>
	/// 볼륨 크기 (0 ~ 1f)
	/// </summary>
	public float Volume;
	/// <summary>
	/// 루프 재생 여부
	/// </summary>
	public bool IsLoop;

	public SoundData(int index, Constant.SoundType type, string name, string filepath, float volume, bool isloop)
	{
		Index = index;
		Type = type;
		Name = name;
		FilePath = filepath;
		Volume = volume;
		IsLoop = isloop;
	}
}

public class SoundTable : ITable
{
	private List<SoundData> _table;
	public ReadOnlyCollection<SoundData> Table => _table.AsReadOnly();

	public void Load()
	{
		string className = GetType().Name;
		CSVReader reader = CSVReader.Load(className);
		if (reader == null)
		{
			Logger.LogErrorFormat("Failed to load {0}.", className);
			return;
		}

		_table = new List<SoundData>();

		for (int i = 3; i < reader.rowCount; i++)
		{
			var row = reader.GetRow(i);
			if (row != null)
			{
				int index = row.GetValue<int>(0);
				Constant.SoundType type = row.GetValue<Constant.SoundType>(1);
				string name = row.GetValue<string>(2);
				string filepath = row.GetValue<string>(3);
				float volume = row.GetValue<float>(4);
				bool isloop = row.GetValue<bool>(5);

				SoundData data = new SoundData(index, type, name, filepath, volume, isloop);
				_table.Add(data);
			}
		}
	}
}
