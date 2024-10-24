using System;

namespace Info
{
    [Serializable]
    public class Status
    {
        public int HP;
        public int ATK;
        public int DEF;
        public int MAG;
        public float SPD;
        public float CRIT;
        public float CRIT_DMG;

        public float AttackRange;
        public float AttackRate;
        public float Dodge;

        public Status(int index, int grade, int level, CharacterStatusData data)
        {
            // 등급&레벨 스케일 적용 스테이터스
            this.HP = BattleCalc.GetStatus(grade, level, data.HP, data.HP_Level, data.HP_Grade);
            this.ATK = BattleCalc.GetStatus(grade, level, data.ATK, data.ATK_Level, data.ATK_Grade);
            this.DEF = BattleCalc.GetStatus(grade, level, data.DEF, data.DEF_Level, data.DEF_Grade);
            this.MAG = BattleCalc.GetStatus(grade, level, data.MAG, data.MAG_Level, data.MAG_Grade);

            this.SPD = data.SPD;
            this.CRIT = data.CRIT;
            this.CRIT_DMG = data.CRIT_DMG;
            this.AttackRange = data.AttackRange;
            this.AttackRate = data.AttackRate;
            this.Dodge = data.DODGE;
        }
    }
}