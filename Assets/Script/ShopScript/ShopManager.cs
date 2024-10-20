using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Transform cardDisplayParent; // �������� ī�带 ǥ���� �θ� ������Ʈ
    public BattleCard cardPrefab; // ī�� ������ (�̸� ����� �� ī��)
    private List<BattleCard> availableCards; // ������ ǥ�õǴ� ī�� ���

    void Start()
    {
        GenerateRandomCards(3); // ��: 3���� ���� ī�带 ����
    }

    // ���� ī�� ���� �Լ�
    public void GenerateRandomCards(int cardCount)
    {
        availableCards = new List<BattleCard>();

        for (int i = 0; i < cardCount; i++)
        {
            int randomCardId = GetRandomCardId(); // ������ cardId�� �������� �Լ�
            BattleCard newCard = Instantiate(cardPrefab, cardDisplayParent);
            newCard.Init(null, randomCardId); // ī�带 �ʱ�ȭ (���⼭ _cardHolder�� null�� ���� ����)

            // �������� �� ī�带 ������ �� �ְ� �ϱ� ���� Ŭ�� �̺�Ʈ�� �߰�
            newCard.GetComponent<Button>().onClick.AddListener(() => OnCardSelected(newCard));

            availableCards.Add(newCard); // ������ ī�带 ������ ī�� ��Ͽ� �߰�
        }
    }

    // ������ cardId�� ��ȯ�ϴ� �Լ� (�ӽ÷� 1~16 ������ ���� �� ��ȯ)
    private int GetRandomCardId()
    {
        return Random.Range(1, 16); // ī�� ID ������ ���� ���� ����
    }

    // ī�� ���� �� ȣ��Ǵ� �Լ�
    public void OnCardSelected(BattleCard selectedCard)
    {
        AddCardToDeck(selectedCard.cardID); // ���õ� ī�带 ���� �߰�
        Destroy(selectedCard.gameObject); // ������ ī��� �������� ����
    }

    // ���� ī�带 �߰��ϴ� �Լ�
    public void AddCardToDeck(int cardId)
    {
        // ���⼭ ���� ī�带 �߰��ϴ� ������ ���� (�� ���� Ŭ������ �ִٸ� �װ��� �߰�)
        Debug.Log($"Card {cardId} added to deck.");
    }
}
