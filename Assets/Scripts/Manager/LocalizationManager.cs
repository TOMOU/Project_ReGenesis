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

    /// <summary>
    /// 테이블을 로드한다.<br>타이틀 까지는 에셋번들 등을 로드하지 않기 때문에...</br>
    /// </summary>
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

    /// <summary>
    /// index에 기반하여 텍스트를 불러온다.<br>index값이 null이거나 텍스트가 빈값이면 NOT_FOUND 출력.</br>
    /// </summary>
    /// <param name="index">불러올 텍스트의 index</param>
    /// <returns></returns>
    public string GetString(int index)
    {
        if (_localizationDic.TryGetValue(index, out LocalizationData data))
        {
            string value = data.Text_KR;
            if (string.IsNullOrEmpty(value) == true)
            {
                return "<color=#ff00ffff>NOT_FOUND : EMPTY</color>";
            }

            return ConvertKeyword(value);
        }

        return "<color=#ff00ffff>NOT_FOUND : NULL</color>";
    }

    private string ConvertKeyword(string text)
    {
        return text.Replace("\"", "").Replace("<username>", "토모우").Replace("<br>", "\n");
    }
}
