namespace ReGenesis.Enums.Character
{
    public enum JobType
    {
        None = 0,
        Tanker,
        Dealer,
        Supporter,
        Healer,
    }

    public enum FormationType
    {
        None = 0,
        Front,
        Middle,
        Back,
    }

    public enum AttackType
    {
        None = 0,
        Melee,
        Range,
    }

    public enum StateFSM
    {
        None,
        Idle,           // 대기
        Run,            // 이동
        Attack,         // 일반공격
        Skill0,         // 궁극기 스킬
        Skill1,         // 일반스킬 1
        Skill2,         // 일반스킬 2
        Stun,           // 기절
        Victory,        // 승리모션
        Die,            // 죽음
        Max
    }

    public enum TeamType
    {
        None,
        Blue,
        Red,
    }
}