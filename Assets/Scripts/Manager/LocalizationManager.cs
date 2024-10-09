using System.Collections.Generic;

public class LocalizationManager : MonoSingleton<LocalizationManager>
{
    private Dictionary<int, LocalizationData> _localizationDic;

    protected override void Init()
    {
        _localizationDic = new Dictionary<int, LocalizationData>();
    }

    protected override void Release()
    {
        _localizationDic?.Clear();
        _localizationDic = null;
    }

    public void LoadTable()
    {
        var table = TableManager.Instance.GetTable<LocalizationTable>();
        if (table != null)
        {
            foreach (var data in table.Table)
            {
                _localizationDic.Add(data.Index, data);
            }
        }
    }

    public string GetString(int index)
    {
        if (_localizationDic.TryGetValue(index, out LocalizationData data))
        {
            return ConvertKeyword(data.Text_KR);
        }

        return "<color=#ff00ffff>NOT_FOUND</color>";
    }

    private string ConvertKeyword(string text)
    {
        return text.Replace("\"", "").Replace("<username>", "토모우").Replace("<br>", "\n");
    }
}
