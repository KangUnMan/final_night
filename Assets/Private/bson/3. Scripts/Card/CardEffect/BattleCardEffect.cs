using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCardEffect
{
    protected BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();
    protected BattlePlayer player => battleManager.Player;
    protected List<Enemy> enemies => battleManager.Enemies;
    protected Enemy targetEnemy => battleManager.TargetEnemy;

    [SerializeField]
    private IndentData[] indentData;

    // ��մ� ��
    public void Strike(BattleCard sender)
    {
        targetEnemy?.Hit(5+ player.PlayerStat.Power, player);
    }

    // ��ȥ �ع�
    public void SoulLiberation(BattleCard sender)
    {
        // ���� �����ϴ��� Ȯ��
        if (targetEnemy == null)
        {
            Debug.LogWarning("Target enemy is null in SoulLiberation. Make sure to set TargetEnemy before using the card.");
            return;
        }

        // ������ ����
        targetEnemy.Hit(8 + player.PlayerStat.Power, player);

        // Weak �ε�Ʈ ������ ��������
        IndentData weakIndentData = indentData[(int)EIndent.Weak];

        if (weakIndentData == null)
        {
            Debug.LogWarning("Weak indent data is null. Please ensure indentData is properly initialized.");
            return;
        }

        // ���� �ε�Ʈ ����Ʈ���� Weak �ε�Ʈ�� ã��
        foreach (var indent in targetEnemy.CharacterIndent.indentList)
        {
            if (indent.indentData.indent == EIndent.Weak)
            {
                // �ε�Ʈ�� �̹� �����ϴ� ��� �ؽ�Ʈ ������Ʈ
                indent.UpdateIndent();
                return;
            }
        }

        // �ε�Ʈ�� �������� �ʴ� ��� ���ο� �ε�Ʈ �߰�
        targetEnemy.CharacterIndent.AddIndent(weakIndentData, 2);
        targetEnemy.CharacterIndent.Visualize(); // �ð��� ������Ʈ�� �ʿ��ϴٸ� �߰�
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
        // �÷��̾��� �Ŀ� ����
        player.PlayerStat.Power += 2;

        // Strength �ε�Ʈ ������ ��������
        IndentData powerIndentData = player.CharacterIndent.GetIndentData(EIndent.Strength);


        foreach (var indent in player.CharacterIndent.indentList)
        {
            if (indent.indentData.indent == EIndent.Strength)
            {
                // �ε�Ʈ�� �̹� �����ϴ� ��� �ؽ�Ʈ ������Ʈ
                indent.UpdateIndent();
                return;
            }
        }

        // �ε�Ʈ �߰�
        player.CharacterIndent.AddIndent(powerIndentData, 2);
        player.CharacterIndent.Visualize();
    }
 }

