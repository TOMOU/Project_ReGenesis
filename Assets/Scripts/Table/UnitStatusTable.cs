using System.Collections.Generic;
using System.Collections.ObjectModel;

public class UnitStatusData
{
    public int Index { get; private set; }              // 캐릭터 고유 인덱스

    public UnitStatusData(int index)
    {
        Index = index;
    }
}

public class UnitStatusTable : ITable
{
    private List<UnitStatusData> _table;
    public ReadOnlyCollection<UnitStatusData> Table => _table.AsReadOnly();

    public void Load()
    {
        string path = string.Format("Table/{0}", GetType().Name);
        CSVReader reader = CSVReader.Load(path);
        if (reader == null)
        {
            Logger.LogErrorFormat("Failed to load [{0}] at [{1}].", GetType().Name, path);
            return;
        }

        _table = new List<UnitStatusData>();

        for (int i = 1; i < reader.rowCount; i++)
        {
            var row = reader.GetRow(i);
            if (row != null)
            {
                int index = row.GetValue<int>(0);

                UnitStatusData unitStatusData = new UnitStatusData(index);
                _table.Add(unitStatusData);
            }
        }
    }
}
