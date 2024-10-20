using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPatternType
{
    Attack,
    Defense,
    Debuff,
    Buff,
    DefendBuff,
    AttackDefend,
    AttackDebuff,
    Unknown,
    Size
}

[CreateAssetMenu()]
public class EnemyPatternData : ScriptableObject
{
    public EPatternType patternType;
    public Sprite patternIcon;
    public string patternName;
    [Multiline(5)]
    public string patternExplanation;
}