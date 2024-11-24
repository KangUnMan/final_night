using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TestAniAct : MonoBehaviour
{
    [SerializeField] private BattlePlayer _player;

    [SerializeField] private List<BattleData> Act1BattleDataList;

    private System.Random  random = new System.Random();

    private int randomNumber;

    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();
    private BattleCardGenerator CardGenerator => ServiceLocator.Instance.GetService<BattleCardGenerator>();

    private void Awake()
    {
        
    }

    private void Start()
    {
        randomNumber = random.Next(0, Act1BattleDataList.Count - 1);
        _player.init();

        for (int i = 1; i < 9; i++)
        {
            if (i<8)
            {
              _player.AddCard(CardGenerator.GenBatCard(i));
            }else {
                _player.AddCard(CardGenerator.GenBatCard(14)); // ��Ƴ��� ���� (���ڸ��� ī�� �׽�Ʈ�غ��� ������ �ɵ�?)
            }
            
            
        }

        battleManager.defeatCommonEnemy = 100;

        battleManager.StartBattle(Act1BattleDataList[randomNumber]);
    }

}
