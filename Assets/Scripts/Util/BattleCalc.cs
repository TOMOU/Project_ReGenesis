using System;

public class BattleCalc
{
    public static int GetStatus(int grade, int level, float origin, float levelScale, float gradeScale)
    {
        float value = origin + origin * ((level - 1) * (levelScale + ((grade - 1) * gradeScale)));
        return (int)Math.Ceiling(value);
    }
}
