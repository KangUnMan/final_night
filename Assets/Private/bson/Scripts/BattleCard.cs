using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECardUsage
{
    Battle,   // ��Ʋ
    Check,    // Ȯ��
    Gain,     // ���
    Enforce,  // ��ȭ
    DisCard,  // ����
    Sell,     // �Ǹ�
}
public class BattleCard : MonoBehaviour
{
    public int generateNumber;

    // ��Ŭ�� �Լ�
    public Action onClickAction;

    [SerializeField]
    private BattleCardController _cardController;

    private BattleCardStateFactory _CardStateFactory;
    private BattleCardHolder _cardHolder;

    public BattleCardState CurrentState => _CardStateFactory.CurrentState;
    public BattleCardHolder CardHolder => _cardHolder;
    public BattleCardController CardController => _cardController;

    public void Init(BattleCardHolder cardHolder, int generateNumber)
    {
        _CardStateFactory = new BattleCardStateFactory(this);

        _cardHolder = cardHolder;

        _cardController.Init(true, this);

        // ���� ������
        this.generateNumber = generateNumber;
    }

    public void ChangeState(ECardUsage cardUsage)
    {
        _CardStateFactory.ChangeState(cardUsage);
    }
}
