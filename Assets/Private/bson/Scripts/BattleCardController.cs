using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleCardController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private float _moveTime = 0.2f;   // �����̴� �ð�

    private Vector3 _targetPos;
    private Vector3 _targetRot;
    private Vector3 _targetScl;

    private Coroutine _coMove;
    private BattleCard _card;

    public bool IsBezierCurve { get; private set; }

    public void Init(bool isBezierCurve , BattleCard battleCard)
    {
        // BaseCard�� ���⼭ �Ѱ�����
        IsBezierCurve = isBezierCurve;
        _card = battleCard;
    }

    public void StopAllCoroutine()
    {
        StopAllCoroutines();
    }

    // ī�带 �巡�� ������ ��
    public void OnBeginDrag(PointerEventData eventData)
    {
        _card.CurrentState.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _card.CurrentState.OnDrag(eventData);
    }

    // �巡�׸� ���� ��(�巡���� �� �ִ� ���� �ƴϿ��� ��)
    public void OnEndDrag(PointerEventData eventData)
    {
        _card.CurrentState.OnEndDrag(eventData);
    }

    // �ش� ī�忡 ���콺�� ������ ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        _card.CurrentState.OnPointerEnter(eventData);
    }

    // �ش� ī�忡 Ŭ������ ��
    public void OnPointerClick(PointerEventData eventData)
    {
        _card.CurrentState.OnPointerClick(eventData);
    }

    // �ش� ī�忡�� ���콺�� ���� ��
    public void OnPointerExit(PointerEventData eventData)
    {
        _card.CurrentState.OnPointerExit(eventData);
    }

    // ī�� �̵�
    public void MoveCard(Vector3 targetPos, Vector3 targetRot, Vector3 targetScl)
    {
        _targetPos = targetPos;
        _targetRot = targetRot;
        _targetScl = targetScl;

        if (_coMove != null)
        {
            StopCoroutine(_coMove);
        }
        _coMove = StartCoroutine(CoMove());
    }

    private IEnumerator CoMove()
    {
        float currentTime = 0f;

        Vector3 originPos = transform.localPosition;
        Vector3 originRot = transform.localEulerAngles;
        Vector3 originScl = transform.localScale;

        while (true)
        {
            currentTime += Time.deltaTime;

            transform.localPosition = Vector3.Lerp(originPos, _targetPos, currentTime / _moveTime);
            transform.localEulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(originRot.z, _targetRot.z, currentTime / _moveTime));
            transform.localScale = Vector3.Lerp(originScl, _targetScl, currentTime / _moveTime);

            if (currentTime >= _moveTime)
                break;

            yield return null;
        }
    }

    public void SetActiveRaycast(bool flag)
    {
        Image[] children = GetComponentsInChildren<Image>();
        foreach (Image child in children)
        {
            child.raycastTarget = flag;
        }
    }
}
