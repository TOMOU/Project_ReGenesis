using System.Collections.Generic;
using System.Collections.ObjectModel;

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

public class LocalizationTable : ITable
{
    private List<LocalizationData> _table;
    public ReadOnlyCollection<LocalizationData> Table => _table.AsReadOnly();

    public void Load()
    {
        string path = string.Format("Table/{0}", GetType().Name);
        CSVReader reader = CSVReader.Load(path);
        if (reader == null)
        {
            Logger.LogErrorFormat("Failed to load [{0}] at [{1}].", GetType().Name, path);
            return;
        }

        _table = new List<LocalizationData>();

        for (int i = 1; i < reader.rowCount; i++)
        {
            var row = reader.GetRow(i);
            if (row != null)
            {
                int index = row.GetValue<int>(0);
                string text_kr = row.GetValue<string>(1);
                string text_jp = row.GetValue<string>(2);
                string text_en = row.GetValue<string>(3);

                LocalizationData data = new LocalizationData(index, text_kr, text_jp, text_en);
                _table.Add(data);
            }
        }
    }
}
