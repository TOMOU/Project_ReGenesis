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

    public void LoadTable()
    {
        _tableDic = new Dictionary<Type, ITable>();

        AddTableDic<SoundTable>();
        AddTableDic<LocalizationTable>();
        AddTableDic<UnitStatusTable>();
    }

    private void AddTableDic<T>() where T : ITable, new()
    {
        T table = new T();
        table.Load();
        _tableDic[typeof(T)] = table;
    }

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
