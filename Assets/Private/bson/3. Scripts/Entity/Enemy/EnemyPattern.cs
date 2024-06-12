using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[System.Serializable]
public class Pattern
{
    public EnemyPatternData patternData;
    public IndentData indentData;
    public int amount;
    public int secondAmount;
}


public class EnemyPattern : MonoBehaviour
{
    private Enemy _enemy;

    public Pattern alreadyPattern;  // �̹� �ִ� ����
    public Pattern enemyFirstPattern;  // ���� ó�� ����
    public List<Pattern> enemyPatterns;  // �� �ܿ� ����
    public List<Pattern> enemyCyclePatterns; // ��ȯ�ϴ� ����
    public bool isAlreadyPattern = false;
    public bool isFirstPattern = false;  // ���� ó�� ������ �ִ°�
    public bool isCyclePattern = false; // ������ ��ȯ�ϴ°�

    [SerializeField] private Image _patternImage;
    [SerializeField] private Text _patternText;

    private bool _isDecided = false;
    private int _patternTurn = 1;
    private Pattern _currentPattern;

    private bool isActFirst = true;

    private VFXGenerator vfxGenerator => ServiceLocator.Instance.GetService<VFXGenerator>();
    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    public void Init(Enemy enemy)
    {
        _enemy = enemy;
        isActFirst = true;
        if (isAlreadyPattern)
        {
            _currentPattern = alreadyPattern;
        }

        if (_currentPattern == null)
        {
            DecidePattern();
            _isDecided = true;
        }
    }

    public void Act()
    {
        // ù��° �Ͽ� �� ������ ������ ����
        ActPattern();

        _patternTurn++;
    }

    public void DecidePattern()
    {
        // ���� �����ϰ� ui�� �����ֱ�
        // �� �� ���� ��������Ʈ�� �� �Լ��� �־���� ��

        // ù��° ������ �ִ� ���̸� ù��° ������ ����� ��
        // �ƴϸ� ����(�ϴ���...)

        if(_isDecided)
        {
            _isDecided = false;
        }
        else if(_patternTurn == 1 && isFirstPattern && isActFirst)
        {
            _currentPattern = enemyFirstPattern;
            isActFirst = false;
            _patternTurn = 0;
        }
        else if(isCyclePattern)
        {
            _currentPattern = enemyCyclePatterns[(_patternTurn - 1) % enemyCyclePatterns.Count];
        }
        else
        {
            _currentPattern = enemyPatterns[Random.Range(0, enemyPatterns.Count)];
        }

        _patternImage.sprite = _currentPattern.patternData.patternIcon;
        _patternText.text = GetPatternAmount();
    }

    public void DecidePattern(Pattern pattern)
    {
        _currentPattern = pattern;
        _isDecided = true;

        _patternImage.sprite = _currentPattern.patternData.patternIcon;
        _patternText.text = "";
    }

    private void ActPattern()
    {
        switch (_currentPattern.patternData.patternType)
        {
            case EPatternType.Attack:
                battleManager.Player.Hit(_currentPattern.amount + _enemy.CharacterStat.Power, _enemy);
                break;
            default:
                Assert.IsTrue(false, "Non PatternType");
                break;
        }
    }

    private string GetPatternAmount()
    {
        string result = "";

        switch (_currentPattern.patternData.patternType)
        {
            case EPatternType.Attack:
            case EPatternType.AttackDefend:
            case EPatternType.AttackDebuff:
                result = (_currentPattern.amount + _enemy.CharacterStat.Power).ToString();
                break;
        }

        return result;
    }
}
