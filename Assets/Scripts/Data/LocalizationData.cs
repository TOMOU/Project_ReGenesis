public class LocalizationData
{
    public int Index { get; private set; }          // 텍스트 고유 인덱스
    public string Text_KR { get; private set; }     // 한국어 텍스트
    public string Text_JP { get; private set; }     // 일본어 텍스트
    public string Text_EN { get; private set; }     // 영어 텍스트

    public LocalizationData(int index, string text_KR, string text_JP, string text_EN)
    {
        Index = index;
        Text_KR = text_KR;
        Text_JP = text_JP;
        Text_EN = text_EN;
    }
}