
public class SkillCardEffect : BaseCardEffect
{
    // ��
    public void barrier()
    {
        player.PlayerStat.Shield += (5 /*+ agility*/);
    }
}
