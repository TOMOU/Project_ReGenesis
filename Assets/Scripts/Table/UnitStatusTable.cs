using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class UnitStatusData
{
    public int Index { get; private set; }
    public string Name { get; private set; }
    public JobType JobType { get; private set; }
    public Position Position { get; private set; }
    public float HP { get; private set; }
    public float HP_Level { get; private set; }
    public float HP_Grade { get; private set; }
    public float ATK { get; private set; }
    public float ATK_Level { get; private set; }
    public float ATK_Grade { get; private set; }
    public float DEF { get; private set; }
    public float DEF_Level { get; private set; }
    public float DEF_Grade { get; private set; }
    public float MAG { get; private set; }
    public float MAG_Level { get; private set; }
    public float MAG_Grade { get; private set; }
    public float SPD { get; private set; }
    public float CRIT { get; private set; }
    public float CRIT_DMG { get; private set; }

    public UnitStatusData(int index, string name, JobType jobType, Position position, float hp, float hp_level, float hp_grade, float atk, float atk_level, float atk_grade, float def, float def_level, float def_grade, float mag, float mag_level, float mag_grade, float spd, float crit, float crit_dmg)
    {
        Index = index;
        Name = name;
        JobType = jobType;
        Position = position;

        HP = hp;
        HP_Level = hp_level;
        HP_Grade = hp_grade;

        ATK = atk;
        ATK_Level = atk_level;
        ATK_Grade = atk_grade;

        DEF = def;
        DEF_Level = def_level;
        DEF_Grade = def_grade;

        MAG = mag;
        MAG_Level = mag_level;
        MAG_Grade = mag_grade;

        SPD = spd;
        CRIT = crit;
        CRIT_DMG = crit_dmg;
    }

    public int GetHP(int grade, int level)
    {
        return GetStatus(grade, level, HP, HP_Level, HP_Grade);
    }

    public int GetATK(int grade, int level)
    {
        return GetStatus(grade, level, ATK, ATK_Level, ATK_Grade);
    }

    public int GetDEF(int grade, int level)
    {
        return GetStatus(grade, level, DEF, DEF_Level, DEF_Grade);
    }

    public int GetMAG(int grade, int level)
    {
        return GetStatus(grade, level, MAG, MAG_Level, MAG_Grade);
    }

    private int GetStatus(int grade, int level, float origin, float levelScale, float gradeScale)
    {
        float value = origin + origin * ((level - 1) * (levelScale + ((grade - 1) * gradeScale)));
        return (int)Math.Ceiling(value);
    }
}

public enum JobType
{
    NONE,
    TANKER,
    DEALER,
    SUPPORT,
    HEALER
}

public enum Position
{
    NONE,
    FRONT,
    MIDDLE,
    BACK
}

public class UnitStatusTable : ITable
{
    private List<UnitStatusData> _table;
    public ReadOnlyCollection<UnitStatusData> Table => _table.AsReadOnly();

    public void Load()
    {
        string path = string.Format("{0}", GetType().Name);
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
                string name = row.GetValue<string>(1);
                JobType jobType = row.GetValue<JobType>(2);
                Position position = row.GetValue<Position>(3);

                float hp = row.GetValue<float>(4);
                float hp_level = row.GetValue<float>(5);
                float hp_grade = row.GetValue<float>(6);

                float atk = row.GetValue<float>(7);
                float atk_level = row.GetValue<float>(8);
                float atk_grade = row.GetValue<float>(9);

                float def = row.GetValue<float>(10);
                float def_level = row.GetValue<float>(11);
                float def_grade = row.GetValue<float>(12);

                float mag = row.GetValue<float>(13);
                float mag_level = row.GetValue<float>(14);
                float mag_grade = row.GetValue<float>(15);

                float spd = row.GetValue<float>(16);
                float crit = row.GetValue<float>(17);
                float crit_dmg = row.GetValue<float>(18);

                UnitStatusData data = new UnitStatusData(
                    index,
                    name,
                    jobType,
                    position,

                    hp,
                    hp_level,
                    hp_grade,

                    atk,
                    atk_level,
                    atk_grade,

                    def,
                    def_level,
                    def_grade,

                    mag,
                    mag_level,
                    mag_grade,

                    spd,
                    crit,
                    crit_dmg);
                _table.Add(data);
            }
        }
    }

    public UnitStatusData GetData(int index)
    {
        UnitStatusData data = _table.Find(e => e.Index == index);
        return data;
    }
}
