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
