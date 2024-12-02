

public class Define
{
    public enum CameraType
    {
        PLAYER, DOWN, MIDDLEBOSS, NONFOLLOW, DEAD, UNDERGROUND, NONE
    }

    public enum BuffType // ENTER : ������ ���� ���� , STAY : ������ �� ������ ����, EXIT : �������� ������ ���� 
    {
        ENTER, STAY, EXIT, NONE
    }

    public enum BuffInfo
    {
        NONE,
        SUPERJUMP,

        #region buff
        SPEEDUP,
        POWERUP,
        HEAL,
        #endregion

        #region debuff
        TICKDAMAGE

        #endregion
        
    }
    
    public enum WarpType
    {
        NONE,
        UNDERGROUND,
        MIDDLEBOSS
    }

    public enum ColliderType
    {
        PERSISTANCE,
        ALTERNATIVE
    }

    public enum DestoryObjectType
    {
        FENCE,
        BOX,
        TRASH,
        FLOOR,
        NONE,
    }

    public enum FoodType
    {
        BANANA,
        CAKE,
        COKE,
        DUMPLINGS,
        HAMBURGER,
        HOTDOG,
        NONE,
    }

    #region �÷��̾�
    public enum TempoType
    {
        MAIN, POINT, NONE
    }

    public enum AttackState
    {
        ATTACK, CHECK, FINISH
    }

    public enum PlayerState
    {
        STUN, DIE, HIT, NONE
    }

    public enum PlayerSfxType
    {
        MAIN, POINT, DASH, RUN, JUMP, STUN, NONE
    }

    #endregion

    #region ����
    public enum PerceptionType
    {
       /* PATROL, BOUNDARY, DETECTIONM, */ // legacy
        IDLE, GUARD, HIT, DETECTIONM, SKILLATTACK, NORMALATTACK, TRACE, DEATH
    }

    #region ����Ʈ ����
    public enum ElitePhaseState
    {
        START, PHASE1, PHASECHANGE, PHASE2, FINISH, NONE
    }
    public enum EliteMonsterState
    {
        IDLE, USESKILL, GROGGY, FAIL, DIE, NONE
    }
    public enum EliteMonsterSkill
    {
        FATTACK, SATTACK, LASER, LAUNCH, RUSH, SUPERPUNCH, BARRIER, THUNDERSTROKE, EXPLOSION, PLATFORM, NONE
    }
    #endregion

    #region �߰� ����
    public enum MiddlePhaseState
    {
        START, PHASE1, PHASECHANGE, PHASE2, FINISH, NONE
    }
    public enum MiddleMonsterState
    {
        IDLE, USESKILL, GROGGY, FAIL, DIE, NONE
    }
    public enum MiddleMonsterSkill
    {
        JONGJUMP, SNIPING, HOMERUN, SHELLING, TAKEDOWN, AIMATTACK, BASESHELLING, WEAPONSWING, SIDESTEP, NONE
    }
    public enum MiddleMonsterPoint
    {
        CSPAWNPOINT, LEFTSIDE, RIGHTSIDE, NONE
    }
    public enum MiddleMonsterName
    {
        CHEONG, GYEONGCHAE, NONE
    }
    #endregion

    #region �Ϲ� ����

    public enum NormalMonsterType
    {
        BALDO, KUNG, MON3, NONE
    }

    #endregion

    #endregion

    #region Skill
    public enum SkillTerms
    {
        NONE,
        INRANGE,
        OUTRANGE
    }

    public enum SkillColliderType
    {
        FORWARD,
        CENTER
    }

    public enum SkillType
    {
        ATK, // ������
        BUF, // ������
        MOV, // �⵿��
    }

    public enum SkillTarget
    {
        NONE,       // ����
        PC,            // Playable Character
        SELF,        // �ڱ��ڽ�
        MON,        // ����
        GROUND, // ����
        ALL,           // ��&�Ʊ�
    }

    public enum SkillEffectType
    {
        NONE,               // ����
        RUSH,               // ����-����
        BONUS_ATK,   // �߰� ����
        TP_TO_PC,       // PC���Է� �����̵�
        CREATE_OBJ  // ���� ������Ʈ ����
    }

    public enum SkillSecondEffectType
    {
        NONE,               // ����
        STIFFNESS,      // ����
        KNOCKBACK,   // �˹�
        KNOCKDOWN // ������
    }
    #endregion
}
