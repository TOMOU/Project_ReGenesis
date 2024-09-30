using System.Collections.Generic;
using System.Collections.ObjectModel;

public class SoundTable : ITable
{
    private List<SoundData> _table;
    public ReadOnlyCollection<SoundData> Table => _table.AsReadOnly();

    public void Load(string path)
    {
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
