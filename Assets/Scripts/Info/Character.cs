using System;

namespace Info
{
    [Serializable]
    public class Character
    {
        public int index;
        public int grade;
        public int level;

        public Info.Status status;

        public Character(int index, int grade, int level, CharacterStatusData data)
        {
            this.index = index;
            this.grade = grade;
            this.level = level;

            status = new Status(index, grade, level, data);
        }
    }
}