using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleCardBattleState : BattleCardState
{
    public static BattlePlayer Instance;
    private bool _isDrag = false;
    private BattlePlayer ani;
    public GameObject a;

    void Awake()
    {
        GameObject parentObject = BattlePlayer.Instance.parentObject;
       
        ani = BattlePlayer.Instance; // �̱����� ���� BattlePlayer �ν��Ͻ� ��������
        if (ani == null)
        {
            Debug.LogError("BattlePlayer �ν��Ͻ��� null�Դϴ�. BattlePlayer�� �ʱ�ȭ�Ǿ����� Ȯ���ϼ���.");
        }
        Debug.LogWarning(ani.parentObject.name);
        if (ani.parentObject == null)
        {
            Debug.LogWarning("�θ�object = null");
        }
        
    }
    public BattleCardBattleState(BattleCard battleCard, BattleCardStateFactory stateFactory) : base(battleCard, stateFactory)
    {
        cardUsage = ECardUsage.Battle;
    }

    public override void Enter()
    {
        
        _isDrag = false;
    }

    public override void Exit()
    {

    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        _isDrag = true;
        
        GlobalSoundManager.Instance.PlaySE(ESE.CardSelect);

        _battleCard.CardHolder.selectedCard = _battleCard;

        _battleCard.CardController.StopAllCoroutine();

        _battleCard.transform.SetAsLastSibling();

        if (_battleCard.CardController.IsBezierCurve)
        {
            _battleCard.CardHolder.BezierCurve.gameObject.SetActive(true);
            _battleCard.CardHolder.MoveCenter(_battleCard);
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (_battleCard.CardController.IsBezierCurve)
        {
            _battleCard.CardHolder.BezierCurve.p0.position = _battleCard.transform.position;
            _battleCard.CardHolder.BezierCurve.p2.position = eventData.position;
        }
        else
        {
            _battleCard.transform.position = eventData.position;
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        _isDrag = false;

        if (_battleCard.CardController.IsBezierCurve) // Ÿ����
        {
            if (battleManager.TargetEnemy != null)
            {
                _battleCard.CardController.SetActiveRaycast(false);
                _battleCard.UseCard();
                
                    BattlePlayer.Instance.attack();
                    Debug.Log("Attack animation triggered.");
                
            }
            
            battleManager.TargetEnemy = null;
            _battleCard.CardHolder.BezierCurve.gameObject.SetActive(false);
        }
        else
        {
            // ��밡��(�ڽ�Ʈ ���)�̰ų� �������� ���� ���� ���
            // ��� ���� y���� 300�̻� -> �̴� �ػ󵵿� ���� �ٲٴ� ������ �ʿ�
            if (eventData.position.y > 300f)
            {
                _battleCard.CardController.SetActiveRaycast(false);
                _battleCard.UseCard();
                BattlePlayer.Instance.magic();
            }
        }

        // �Ұ� �� �ϰ� nulló��
        _battleCard.CardHolder.selectedCard = null;
        // ������ �� ������ � ��Ȱ��ȭ
        _battleCard.CardHolder.Relocation();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (_isDrag)
            return;

        _battleCard.CardHolder.OverCard(_battleCard);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (_isDrag)
            return;

        _battleCard.CardHolder.Relocation();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        _battleCard.onClickAction?.Invoke();
    }
}
