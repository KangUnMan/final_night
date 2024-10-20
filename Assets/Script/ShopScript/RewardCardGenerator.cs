using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class RewardCardGenerator : MonoBehaviour
{
    [SerializeField] BattleCard _defaultDummyCard;
    [SerializeField] private Transform _cardParent;
    [SerializeField] private BattleCardHolder _cardHolder;


    public BattleCard GetRandomCard()
    {
        GenerateRandomCard randomCardIdGenerator = FindObjectOfType<GenerateRandomCard>();

        int cardId = randomCardIdGenerator.GetUniqueRandomCardId();


        return GenBatCard(cardId); // �������� ���õ� ī�� ��ȯ
    }


    public BattleCard GenBatCard(int cardId)
    {
        if (
            !(ResourceManager.Instance.AttackCardIdList.Contains(cardId)
            || ResourceManager.Instance.SkillCardIdList.Contains(cardId)
            || ResourceManager.Instance.HeroCardIdList.Contains(cardId))
            )
        {
            Assert.IsTrue(false, "ī�带 �����ϴµ� �ʿ��� ���ҽ��� �ε���� ����");
        }

        BattleCard genCard = Instantiate(_defaultDummyCard, _cardParent);
        //updateCardResource(genCard.gameObject.transform, cardId);
        genCard.Init(_cardHolder, cardId);

        return genCard;
    }
}
