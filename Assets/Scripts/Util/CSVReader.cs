using System;
using UnityEngine;

public class CSVReader
{
    public class Row
    {
        public string[] cell;

        public T GetValue<T>(int i)
        {
            if (i >= cell.Length || string.IsNullOrEmpty(cell[i]) == true)
                return default;

            string value = cell[i].Trim();

            // T 형식에 따라 다른 값을 리턴
            return typeof(T) switch
            {
                Type t when t == typeof(int) => (T)(object)(int.TryParse(value, out int intValue) ? intValue : default),
                Type t when t == typeof(uint) => (T)(object)(uint.TryParse(value, out uint uintValue) ? uintValue : default),
                Type t when t == typeof(float) => (T)(object)(float.TryParse(value, out float floatValue) ? floatValue : default),
                Type t when t == typeof(long) => (T)(object)(long.TryParse(value, out long longValue) ? longValue : default),
                Type t when t == typeof(byte) => (T)(object)(byte.TryParse(value, out byte byteValue) ? byteValue : default),
                Type t when t == typeof(double) => (T)(object)(double.TryParse(value, out double doubleValue) ? doubleValue : default),
                Type t when t == typeof(bool) => (T)(object)(value.Equals("TRUE", System.StringComparison.OrdinalIgnoreCase)),
                Type t when t.IsEnum => Enum.TryParse(typeof(T), value, out var enumValue) ? (T)enumValue : default,
                _ => (T)(object)value,
            };
        }
    }

    private Row[] rows;
    public int rowCount;
    public int colCount;

    private CSVReader(string data)
    {
        ParseCSV(data);
    }

    public static CSVReader Load(string path)
    {
        TextAsset asset = Resources.Load(path) as TextAsset;
        string data = System.Text.Encoding.UTF8.GetString(asset.bytes);
        return new CSVReader(data);
    }

    public static CSVReader Load(TextAsset asset)
    {
        return new CSVReader(asset.text);
    }

    private void ParseCSV(string data)
    {
        // UTF-8 BOM 제거 및 줄바꿈
        data = data.Trim('\uFEFF', '\u200B').Replace("\r\n", "\n").Replace("\r", "\n");
        string[] lines = data.Split('\n');

        rowCount = lines.Length;
        rows = new Row[rowCount];

        for (int i = 0; i < rowCount; i++)
        {
            rows[i] = new Row();
            rows[i].cell = lines[i].Split(',');

            // 최대 열 수 계산
            colCount = Mathf.Max(colCount, rows[i].cell.Length);
        }
    }

    public Row GetRow(int i)
    {
        return (i >= 0 && i < rowCount) ? rows[i] : null;
    }
}