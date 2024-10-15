using System.Collections.Generic;
using System.Collections.ObjectModel;

// ==================================================
// 자동 생성된 코드입니다. 임의로 수정하지 마세요.
// ==================================================
public class CharacterData
{
	/// <summary>
	/// 캐릭터 고유 인덱스
	/// </summary>
	public int Index;
	/// <summary>
	/// 캐릭터 이름 인덱스
	/// </summary>
	public int Name;
	/// <summary>
	/// 캐릭터 설명 인덱스
	/// </summary>
	public int Description;
	/// <summary>
	/// 스파인 폴더이름
	/// </summary>
	public string SpineFolderName;
	/// <summary>
	/// 전신 이미지 경로
	/// </summary>
	public string ImagePath_Fullbody;
	/// <summary>
	/// 반신 이미지 경로
	/// </summary>
	public string ImagePath_Halfbody;
	/// <summary>
	/// 얼굴 아이콘 경로
	/// </summary>
	public string ImagePath_Face;

	public CharacterData(int index, int name, int description, string spinefoldername, string imagepath_fullbody, string imagepath_halfbody, string imagepath_face)
	{
		Index = index;
		Name = name;
		Description = description;
		SpineFolderName = spinefoldername;
		ImagePath_Fullbody = imagepath_fullbody;
		ImagePath_Halfbody = imagepath_halfbody;
		ImagePath_Face = imagepath_face;
	}
}

public class CharacterTable : ITable
{
	private List<CharacterData> _table;
	public ReadOnlyCollection<CharacterData> Table => _table.AsReadOnly();

	public void Load()
	{
		string className = GetType().Name;
		CSVReader reader = CSVReader.Load(className);
		if (reader == null)
		{
			Logger.LogErrorFormat("Failed to load {0}.", className);
			return;
		}

		_table = new List<CharacterData>();

		for (int i = 3; i < reader.rowCount; i++)
		{
			var row = reader.GetRow(i);
			if (row != null)
			{
				int index = row.GetValue<int>(0);
				int name = row.GetValue<int>(1);
				int description = row.GetValue<int>(2);
				string spinefoldername = row.GetValue<string>(3);
				string imagepath_fullbody = row.GetValue<string>(4);
				string imagepath_halfbody = row.GetValue<string>(5);
				string imagepath_face = row.GetValue<string>(6);

				CharacterData data = new CharacterData(index, name, description, spinefoldername, imagepath_fullbody, imagepath_halfbody, imagepath_face);
				_table.Add(data);
			}
		}
	}
}
