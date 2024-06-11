using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCardStateFactory
{
    private BattleCard _card;
    private Dictionary<ECardUsage, BattleCardState> _dicState = new Dictionary<ECardUsage, BattleCardState>();

    private BattleCardState _currentState;

    public BattleCardState CurrentState => _currentState;

    public BattleCardStateFactory(BattleCard battleCard)
    {
        _card = battleCard;

        _dicState[ECardUsage.Battle] = new BattleCardBattleState(_card, this);
        _dicState[ECardUsage.Gain] = new BattleCardGainState(_card, this);

        // ���� ó���� ��Ʋ���·� �ʱ�ȭ
        ChangeState(ECardUsage.Battle);
    }

    public void ChangeState(ECardUsage cardState)
    {
        BattleCardState newState = GetState(cardState);

        if (_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = newState;

        if (_currentState != null)
        {
            _currentState.Enter();
        }

        Debug.Log(_currentState + "�� ���°� ��ȯ�Ǿ����ϴ�.");
    }

    public BattleCardState GetState(ECardUsage cardState)
    {
        if (!_dicState.ContainsKey(cardState))
        {
            Debug.Log("�߸��� Ű �Է��Դϴ�.");
            return null;
        }

        return _dicState[cardState];
    }
}
