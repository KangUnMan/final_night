using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCardEffect
{
    protected BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();
    protected BattlePlayer player => battleManager.Player;
    protected List<Enemy> enemies => battleManager.Enemies;
    protected Enemy targetEnemy => battleManager.TargetEnemy;

    public void Strike(BattleCard sender)
    {
        targetEnemy.Hit(5, player);
    }

    public void SoulLiberation(BattleCard sender)
    {
        targetEnemy.Hit(13, player);
    }

    // ��
    public void barrier(BattleCard sender)
    {
        player.PlayerStat.Shield += (5 /*+ agility*/);
    }

    public void GrowthAttackDamage(BattleCard sender)
    {
        targetEnemy.Hit(sender.EffectValues[0], player);
        sender.EffectValues[0]++;
    }
}
