using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempPlayer : MonoBehaviour
{
    public List<BattleCard> myCards;
    public BattleCardHolder cardHolder;


    public void Init()
    {
        
    }

    public void OnStartBattle()
    {
        cardHolder.StartBattle(myCards);
    }

    public void OnEndBattle()
    {
        cardHolder.EndBattle(myCards);
    }

    public void ResumeBattle()
    {
        cardHolder.ResumeBattle(myCards);
    }


    // �÷��̾��� ī�带 �����ش�.
    public void AddCard(BattleCard card)
    {
        myCards.Add(card);
    }

    // �÷��̾��� ī�带 �����Ѵ�.
    public void RemoveCard(BattleCard card)
    {

    }

}
