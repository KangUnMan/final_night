using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCardEffect
{
    protected BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();
    protected BattlePlayer player => battleManager.Player;
    protected List<Enemy> enemies => battleManager.Enemies;
    protected Enemy targetEnemy => battleManager.TargetEnemy;

    // ��մ� ��
    public void Strike(BattleCard sender)
    {
        targetEnemy?.Hit(5+ player.PlayerStat.Power, player);
    }

    // ��ȥ �ع�
    public void SoulLiberation(BattleCard sender)
    {
        targetEnemy?.Hit(13+ player.PlayerStat.Power, player);
    }

    // ���� ��
    public void barrier(BattleCard sender)
    {
        player.PlayerStat.Shield += (5 /*+ agility*/);
    }

    public void GrowthAttackDamage(BattleCard sender)
    {
        targetEnemy?.Hit(sender.EffectValues[0], player);
        sender.EffectValues[0]++;
    }

    // ������
    public void AreaAttack(BattleCard sender)
    {
        foreach (var enemy in enemies)
        {
            enemy.Hit(10 + player.PlayerStat.Power, player);
        }
    }

    // ���� ��ȯ
    public void ManaCirculation(BattleCard sender)
    {
        player.PlayerStat.CurrentOrb += 2;
    }

    //����
    public void EvilEye(BattleCard sender)
    {
        player.PlayerStat.Power += 2;
    }
}
