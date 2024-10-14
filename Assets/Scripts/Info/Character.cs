using ReGenesis.Enums.Character;
using System;

namespace ReGenesis.Info
{
    public class Character
    {
        public int grade;
        public int level;

        public int index;
        public string name;
        public JobType jobType;
        public FormationType formationType;
        public AttackType attackType;
        public int hp;
        public int atk;
        public int def;
        public int mag;
        public float speed;
        public float critical;
        public float criticalDamage;

        /// <summary>
        /// 테이블 데이터를 기준으로 캐릭터정보 초기화
        /// </summary>
        /// <param name="data"></param>
        public Character(int grade, int level, CharacterStatusData data)
        {
            this.grade = grade;
            this.level = level;

            // 기본 정보
            this.index = data.Index;
            this.name = data.Name;
            this.jobType = data.JobType;
            this.formationType = data.FormationType;
            this.attackType = data.AttackType;

            // 아래 4개 스테이터스는 등급&레벨에 따라 계산이 달라짐.
            this.hp = GetStatus(data.HP, data.HP_Level, data.HP_Grade);
            this.atk = GetStatus(data.ATK, data.ATK_Level, data.ATK_Grade);
            this.def = GetStatus(data.DEF, data.DEF_Level, data.DEF_Grade);
            this.mag = GetStatus(data.MAG, data.MAG_Level, data.MAG_Grade);

            // 아래 3개는 장비 이외에는 변화 없음.
            this.speed = data.SPD;
            this.critical = data.CRIT;
            this.criticalDamage = data.CRIT_DMG;
        }

        /// <summary>
        /// 등급&레벨에 따라 스테이터스 변화.
        /// <br>계산결과는 올림 처리.</br>
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="levelScale"></param>
        /// <param name="gradeScale"></param>
        /// <returns></returns>
        public int GetStatus(float origin, float levelScale, float gradeScale)
        {
            float value = origin + ((level - 1) * (levelScale + ((grade - 1) * gradeScale)));
            return (int)Math.Ceiling(value);
        }
    }
}