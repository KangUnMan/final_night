using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCardManager : MonoBehaviour
{
    private BattleCardGenerator cardGenerator => ServiceLocator.Instance.GetService<BattleCardGenerator>();


    [SerializeField]
    private GameObject cardShopGameObject;
    [SerializeField]
    private Transform cardShopParent;



    // ī�� 3�� ����
    private void GetCard()
    {
        if (cardGenerator == null)
        {
            Debug.LogError("BattleCardGenerator not found!");
            return;
        }

        BattleCard card1 = cardGenerator.GetRandomCard();
        BattleCard card2 = cardGenerator.GetRandomCard();
        BattleCard card3 = cardGenerator.GetRandomCard();

        card1.ChangeState(ECardUsage.Gain);
        card2.ChangeState(ECardUsage.Gain);
        card3.ChangeState(ECardUsage.Gain);

        card1.onClickAction = null;
        card2.onClickAction = null;
        card3.onClickAction = null;

        card1.onClickAction += (() => OnClickGainCard(card1));
        card2.onClickAction += (() => OnClickGainCard(card2));
        card3.onClickAction += (() => OnClickGainCard(card3));

        card1.transform.SetParent(cardShopParent);
        card2.transform.SetParent(cardShopParent);
        card3.transform.SetParent(cardShopParent);

        card1.transform.localScale = Vector3.one;
        card2.transform.localScale = Vector3.one;
        card3.transform.localScale = Vector3.one;


    }

    // ���� ī�带 ������ �� ����� �Լ�
    private void OnClickGainCard(BattleCard clickedCard)
    {
        int cardID = clickedCard.cardID;
        Debug.Log("Ŭ���� ī�� ID: " + cardID);
        UserManager.Instance.CardDeckIndex.Add(cardID);
    }

}
