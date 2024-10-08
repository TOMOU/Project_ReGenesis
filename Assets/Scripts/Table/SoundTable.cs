using System.Collections.Generic;
using System.Collections.ObjectModel;

public class SoundData
{
    public int Index { get; private set; }         // 사운드 고유 인덱스
    public SoundType Type { get; private set; }    // 사운드 타입 (BGM, SFX, Voice 등)
    public string Name { get; private set; }       // 사운드 이름
    public string FilePath { get; private set; }   // 사운드 파일 이름 또는 경로
    public float Volume { get; private set; }      // 볼륨 크기 (0.0f ~ 1.0f)
    public bool IsLoop { get; private set; }       // 루프 여부

    public SoundData(int index, SoundType type, string name, string filePath, float volume, bool isLoop)
    {
        Index = index;
        Type = type;
        Name = name;
        FilePath = filePath;
        Volume = volume;
        IsLoop = isLoop;
    }
}

// 사운드 타입을 정의하는 열거형 (BGM, SFX, VOICE 등)
public enum SoundType
{
    BGM,
    SFX,
    Voice
}


public class SoundTable : ITable
{
    private List<SoundData> _table;
    public ReadOnlyCollection<SoundData> Table => _table.AsReadOnly();

    public void Load()
    {
        string path = string.Format("Table/{0}", GetType().Name);
        CSVReader reader = CSVReader.Load(path);
        if (reader == null)
        {
            Logger.LogErrorFormat("Failed to load [{0}] at [{1}].", GetType().Name, path);
            return;
        }

        _table = new List<SoundData>();

        for (int i = 1; i < reader.rowCount; i++)
        {
            var row = reader.GetRow(i);
            if (row != null)
            {
                int index = row.GetValue<int>(0);
                SoundType type = row.GetValue<SoundType>(1);
                string name = row.GetValue<string>(2);
                string filePath = row.GetValue<string>(3);
                float volume = row.GetValue<float>(4);
                bool isLoop = row.GetValue<bool>(5);

                SoundData soundData = new SoundData(index, type, name, filePath, volume, isLoop);
                _table.Add(soundData);
            }
        }
    }
}
