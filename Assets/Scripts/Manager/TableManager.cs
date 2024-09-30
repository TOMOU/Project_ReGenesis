using System;
using System.Collections.Generic;

public class TableManager : MonoSingleton<TableManager>
{
    private Dictionary<Type, ITable> _tableDic;

    protected override void Init()
    {
        _tableDic = new Dictionary<Type, ITable>();

        SoundTable soundTable = new SoundTable();
        soundTable.Load("SoundTable.csv");
        _tableDic[typeof(SoundTable)] = soundTable;
    }

    protected override void Release()
    {
        _tableDic.Clear();
        _tableDic = null;
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
