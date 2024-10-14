using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class TableCodeGenerator
{
    [MenuItem("Tools/Table/Generate Table Code")]
    public static void Generate()
    {
        // 폴더 경로
        string tableFolderPath = Path.Combine(Application.dataPath, "AssetBundles/Table");

        // 폴더 유무 확인
        if (Directory.Exists(tableFolderPath) == false)
        {
            Logger.LogErrorFormat("폴더 경로를 찾을 수 없습니다: {0}", tableFolderPath);
            return;
        }

        // 폴더 내 모든 csv파일 찾기
        string[] files = Directory.GetFiles(tableFolderPath, "*.csv");
        if (files.Length == 0)
        {
            Logger.LogWarning("CSV 파일이 없습니다.");
            return;
        }

        for (int i = 0; i < files.Length; i++)
        {
            string className = Path.GetFileNameWithoutExtension(files[i]);
            GenerateScript(className);
        }
    }

    private static void GenerateScript(string className)
    {
        // 스크립트가 저장될 경로
        string scriptPath = Path.Combine(Application.dataPath, $"Scripts/Table/{className}.cs");

        // Data 클래스의 이름
        string dataName = className.Replace("Table", "Data");

        // CSV 파일 읽기
        CSVReader reader = CSVReader.LoadEditor(className);
        if (reader == null)
        {
            Logger.LogErrorFormat("Failed to load [{0}].", className);
            return;
        }

        // 코드 생성 시작
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine("using System.Collections.ObjectModel;");
        sb.AppendLine();
        sb.AppendLine("// ==================================================");
        sb.AppendLine("// 자동 생성된 코드입니다. 임의로 수정하지 마세요.");
        sb.AppendLine("// ==================================================");

        // 데이터 클래스
        GenerateData(sb, reader, className, dataName);

        // 테이블 클래스
        GenerateClass(sb, reader, className, dataName);

        //파일 작성 및 갱신
        File.WriteAllText(scriptPath, sb.ToString());
        AssetDatabase.Refresh();

        Logger.LogFormat("{0} 스크립트의 생성 완료!", className);
    }

    private static void GenerateData(StringBuilder sb, CSVReader reader, string className, string dataName)
    {
        var comment = reader.GetRow(0);
        var type = reader.GetRow(1);
        var name = reader.GetRow(2);

        sb.AppendLine($"public class {dataName}");
        sb.AppendLine("{");
        for (int i = 0; i < reader.colCount; i++)
        {
            // 데이터형이 없으면 주석으로 간주하고 건너뛴다.
            if (string.IsNullOrEmpty(type.GetValue<string>(i)) == true)
            {
                continue;
            }

            sb.AppendLine("\t/// <summary>");
            sb.AppendLine($"\t/// {comment.GetValue<string>(i)}");
            sb.AppendLine("\t/// </summary>");
            sb.AppendLine($"\tpublic {type.GetValue<string>(i)} {name.GetValue<string>(i)};");
        }
        sb.AppendLine();
        sb.Append($"\tpublic {dataName}(");
        for (int i = 0; i < reader.colCount; i++)
        {
            // 데이터형이 없으면 주석으로 간주하고 건너뛴다.
            if (string.IsNullOrEmpty(type.GetValue<string>(i)) == true)
            {
                continue;
            }

            if (i > 0)
            {
                sb.Append(", ");
            }

            sb.Append($"{type.GetValue<string>(i)} {name.GetValue<string>(i).ToLower()}");
        }
        sb.AppendLine(")");
        sb.AppendLine("\t{");
        for (int i = 0; i < reader.colCount; i++)
        {
            // 데이터형이 없으면 주석으로 간주하고 건너뛴다.
            if (string.IsNullOrEmpty(type.GetValue<string>(i)) == true)
            {
                continue;
            }

            sb.AppendLine($"\t\t{name.GetValue<string>(i)} = {name.GetValue<string>(i).ToLower()};");
        }
        sb.AppendLine("\t}");
        sb.AppendLine("}");
        sb.AppendLine();
    }

    private static void GenerateClass(StringBuilder sb, CSVReader reader, string className, string dataName)
    {
        var type = reader.GetRow(1);
        var name = reader.GetRow(2);

        sb.AppendLine($"public class {className} : ITable");
        sb.AppendLine("{");
        sb.AppendLine($"\tprivate List<{dataName}> _table;");
        sb.AppendLine($"\tpublic ReadOnlyCollection<{dataName}> Table => _table.AsReadOnly();");
        sb.AppendLine();
        sb.AppendLine("\tpublic void Load()");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tstring className = GetType().Name;");
        sb.AppendLine("\t\tCSVReader reader = CSVReader.Load(className);");
        sb.AppendLine("\t\tif (reader == null)");
        sb.AppendLine("\t\t{");
        sb.AppendLine("\t\t\tLogger.LogErrorFormat(\"Failed to load {0}.\", className);");
        sb.AppendLine("\t\t\treturn;");
        sb.AppendLine("\t\t}");
        sb.AppendLine();
        sb.AppendLine($"\t\t_table = new List<{dataName}>();");
        sb.AppendLine();
        sb.AppendLine("\t\tfor (int i = 3; i < reader.rowCount; i++)");
        sb.AppendLine("\t\t{");
        sb.AppendLine("\t\t\tvar row = reader.GetRow(i);");
        sb.AppendLine("\t\t\tif (row != null)");
        sb.AppendLine("\t\t\t{");

        for (int i = 0; i < reader.colCount; i++)
        {
            string t = type.GetValue<string>(i);
            string n = name.GetValue<string>(i).ToLower();

            // 데이터형이 없으면 주석으로 간주하고 건너뛴다.
            if (string.IsNullOrEmpty(t) == true)
            {
                continue;
            }

            sb.AppendLine($"\t\t\t\t{t} {n} = row.GetValue<{t}>({i});");
        }
        sb.AppendLine();

        sb.Append($"\t\t\t\t{dataName} data = new {dataName}(");
        for (int i = 0; i < reader.colCount; i++)
        {
            // 데이터형이 없으면 주석으로 간주하고 건너뛴다.
            if (string.IsNullOrEmpty(type.GetValue<string>(i)) == true)
            {
                continue;
            }

            if (i > 0)
            {
                sb.Append(", ");
            }

            string n = name.GetValue<string>(i).ToLower();

            sb.Append($"{n}");
        }
        sb.AppendLine(");");

        sb.AppendLine("\t\t\t\t_table.Add(data);");
        sb.AppendLine("\t\t\t}");
        sb.AppendLine("\t\t}");
        sb.AppendLine("\t}");
        sb.AppendLine("}");
    }
}