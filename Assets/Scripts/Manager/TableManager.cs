using System;
using System.Collections.Generic;

public class TableManager : MonoSingleton<TableManager>
{
    private Dictionary<Type, ITable> _tableDic;

    protected override void Init()
    {

    }

    protected override void Release()
    {
        _tableDic.Clear();
        _tableDic = null;
    }

    /// <summary>
    /// 테이블을 로드한다.<br>타이틀 까지는 에셋번들 등을 로드하지 않기 때문에...</br>
    /// </summary>
    public void LoadTable()
    {
        _tableDic = new Dictionary<Type, ITable>();

        AddTableDic<SoundTable>();
        AddTableDic<LocalizationTable>();
        AddTableDic<CharacterStatusTable>();
        AddTableDic<CharacterTable>();
    }

    /// <summary>
    /// _tableDic에 T:ITable 데이터를 저장한다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private void AddTableDic<T>() where T : ITable, new()
    {
        T table = new T();
        table.Load();
        _tableDic[typeof(T)] = table;
    }

    /// <summary>
    /// 외부에서 T:ITable 데이터를 읽는다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetTable<T>() where T : class, ITable
    {
        if (_tableDic.TryGetValue(typeof(T), out ITable table))
        {
            return table as T;
        }
        else
        {
            Logger.LogErrorFormat("Table name [{0}] not found.", typeof(T));
            return null;
        }
    }
}
