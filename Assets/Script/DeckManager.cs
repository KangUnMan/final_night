using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<CardData> deck = new List<CardData>();

    // ���� ī�带 �߰��ϴ� �Լ�
    public void AddCardToDeck(CardData card)
    {
        deck.Add(card);
    }

    // ������ ī�带 �����ϴ� �Լ�
    public void RemoveCardFromDeck(CardData card)
    {
        deck.Remove(card);
    }
}
