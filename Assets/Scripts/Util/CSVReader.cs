using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
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

    /// <summary>
    /// 에셋번들 경로에서 CSV 파일을 읽는다.<br>여기서 가공된 데이터를 기반으로 테이블을 저장한다.</br>
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static CSVReader Load(string name)
    {
        TextAsset asset = ResourceManager.Instance.LoadTextAsset(name);
        if (asset == null)
        {
            Logger.LogErrorFormat("Failed to load CSV File: {0}", name);
            return null;
        }

        string data = System.Text.Encoding.UTF8.GetString(asset.bytes);
        return new CSVReader(data);
    }

    public static CSVReader LoadEditor(string name)
    {
        TextAsset asset = AssetDatabase.LoadAssetAtPath<TextAsset>($"Assets/AssetBundles/Table/{name}.csv");
        if (asset == null)
        {
            Logger.LogErrorFormat("Failed to load CSV File: {0}", name);
            return null;
        }

        string data = System.Text.Encoding.UTF8.GetString(asset.bytes);
        return new CSVReader(data);
    }

    /// <summary>
    /// CSV 데이터를 행 단위로 분리하여 각 행을 Row 객체로 저장한다.
    /// </summary>
    /// <param name="data"></param>
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
            rows[i].cell = ParseLine(lines[i]);

            // 최대 열 수 계산
            colCount = Mathf.Max(colCount, rows[i].cell.Length);
        }
    }

    /// <summary>
    /// 주어진 CSV 행 데이터를 셀 단위로 분리한다.
    /// <br>쌍따옴표는 무시.</br>
    /// <br>쉼표는 구분자로 사용한다.</br>
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    private string[] ParseLine(string line)
    {
        List<string> result = new List<string>();
        StringBuilder currentCell = new StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            // 쌍따옴표 상태 전환. 이 상태에서 쉼표 시 split하지 않음
            if (c == '\"')
            {
                inQuotes = !inQuotes;
            }
            // 쌍따옴표 상태가 아니면서 쉼표가 나오면 result에 추가
            else if (c == ',' && !inQuotes)
            {
                result.Add(currentCell.ToString().Trim('\"'));
                currentCell.Clear();
            }
            // 쌍따옴표 상태에선 currentCell에 한글자씩 추가
            else
            {
                currentCell.Append(c); // 현재 셀에 문자 추가
            }
        }

        // 마지막 셀 추가
        if (currentCell.Length > 0)
        {
            result.Add(currentCell.ToString().Trim('\"'));
        }

        return result.ToArray();
    }

    /// <summary>
    /// 지정된 인덱스에 해당하는 Row을 반환한다.
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public Row GetRow(int i)
    {
        return (i >= 0 && i < rowCount) ? rows[i] : null;
    }
}