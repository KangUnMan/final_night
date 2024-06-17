using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EBGM
{
    Menu,
    EventScene,
    Act1,
    Rest,
    EliteMeet,
    PlayerDead,
    BossMeet,
    BossClear,
    Size
}

public enum ESE
{
    UIHover,
    UIClick,
    CardSelect,      // ī�� onDragEnter �� ��
    BuyItem,         // ������ ����
    OpenTreasureBox, // ���� ����
    StartMyTun,      // �� �� ����
    PressEndButton,  // �� �� ���� ��ư ������
    StartEnemyTurn,  // �� �� ����
    Heal,            // ȸ��
    ShowMap,         // �� UIų ��
    EnterRoom,       // �� �� ��
    CardHover,       // ī�� ȣ��
    NormalAttack,    // �⺻ ����
    Buff,            // ����
    Debuff,          // �����
    EnemyAttack,     // �� ����
    Size,
}

public class GlobalSoundManager : MonoBehaviour
{
    public static GlobalSoundManager Instance { get; set; }
    
    private Dictionary<EBGM, AudioClip> _bgmDic;
    private Dictionary<ESE, AudioClip> _seDic;

    private AudioSource _bgmAudio = null;
    private AudioSource _seAudio = null;

    public float bgmVolume = 0.5f;
    public float seVolume = 0.5f;

    private Coroutine _coBGM;

    private void Awake()
    {
        if (GlobalSoundManager.Instance == null)
        {
            GlobalSoundManager.Instance = this;
            DontDestroyOnLoad(gameObject);

            init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void init()
    {
        if (_bgmAudio == null)
        {
            GameObject go = new GameObject("BGMSound");
            go.transform.parent = gameObject.transform;
            _bgmAudio = go.AddComponent<AudioSource>();
            _bgmAudio.loop = true;
            GameObject go1 = new GameObject("EffectSound");
            go1.transform.parent = gameObject.transform;
            _seAudio = go1.AddComponent<AudioSource>();
            _seAudio.loop = false;
        }

        _bgmDic = new Dictionary<EBGM, AudioClip>();

        _bgmDic[EBGM.Menu] = Resources.Load<AudioClip>("BGM/Piano Instrumental 3_SunSet");
        _bgmDic[EBGM.EventScene] = Resources.Load<AudioClip>("BGM/RPG_Dungeon");
        _bgmDic[EBGM.Act1] = Resources.Load<AudioClip>("BGM/STS_Level1_NewMix_v1");
        
        _seDic = new Dictionary<ESE, AudioClip>();
    
        //ui
        _seDic[ESE.UIHover] = Resources.Load<AudioClip>("SE/SOTE_SFX_UIHover_v2");
        _seDic[ESE.UIClick] = Resources.Load<AudioClip>("SE/SOTE_SFX_UIClick_2_v2");
        
        //������
        _seDic[ESE.CardSelect] = Resources.Load<AudioClip>("SE/SOTE_SFX_CardSelect_v2");
        _seDic[ESE.BuyItem] = Resources.Load<AudioClip>("SE/SOTE_SFX_CashRegister");
        _seDic[ESE.OpenTreasureBox] = Resources.Load<AudioClip>("SE/SOTE_SFX_ChestOpen_v2");
        _seDic[ESE.PressEndButton] = Resources.Load<AudioClip>("SE/SOTE_SFX_EndTurn_v2");
        _seDic[ESE.StartEnemyTurn] = Resources.Load<AudioClip>("SE/SOTE_SFX_EnemyTurn_v3");
        _seDic[ESE.Heal] = Resources.Load<AudioClip>("SE/SOTE_SFX_HealShort_1_v2");
        _seDic[ESE.ShowMap] = Resources.Load<AudioClip>("SE/SOTE_SFX_Map_1_v3");
        _seDic[ESE.EnterRoom] = Resources.Load<AudioClip>("SE/SOTE_SFX_MapSelect_1_v1");
        _seDic[ESE.StartMyTun] = Resources.Load<AudioClip>("SE/SOTE_SFX_PlayerTurn_v4_1");
        _seDic[ESE.CardHover] = Resources.Load<AudioClip>("SE/STS_SFX_CardHover3_v1");

        // ����
        _seDic[ESE.NormalAttack] = Resources.Load<AudioClip>("SE/STS_SFX_DaggerThrow_2_2");
        _seDic[ESE.Buff] = Resources.Load<AudioClip>("SE/SOTE_SFX_Buff_1_v1");
        _seDic[ESE.Debuff] = Resources.Load<AudioClip>("SE/SOTE_SFX_Debuff_1_v1");

        // ��
        _seDic[ESE.EnemyAttack] = Resources.Load<AudioClip>("SE/STS_SFX_Shiv2_v1");
    }

    private void Start()
    {
        GlobalSoundManager.Instance.ChangeBGMVolume(0.5f);
        GlobalSoundManager.Instance.FadeBGM(EBGM.Menu);
    }

    public void PlaySE(ESE se)
    {
        _seAudio.PlayOneShot(_seDic[se]);
    }

    // ����� ���
    public void PlayBGM(EBGM bgm)
    {
        if(!_bgmDic.ContainsKey(bgm))
        {
            Debug.Log("���� bgm �Դϴ�.");
            return;
        }

        if (_bgmAudio.clip == _bgmDic.ContainsKey(bgm))
        {
            Debug.Log("������ clip�Դϴ�.");
            return;
        }

        _bgmAudio.Stop();
        _bgmAudio.clip = _bgmDic[bgm];
        _bgmAudio.Play();
    }

    // ����� �ߴ�
    public void StopBgm()
    {
        _bgmAudio.Stop();
    }

    public void ChangeBGMVolume(float value)
    {
        _bgmAudio.volume = value;
    }

    public void ChangeSEVolume(float value)
    {
        _seAudio.volume = value;
    }

    public void FadeBGM(EBGM bgm, float volume = 0.5f, float duration = 1.5f)
    {
        if (_coBGM != null)
        {
            StopCoroutine(_coBGM);
        }

        bgmVolume = volume;
        
        _coBGM = StartCoroutine(FadeInOutAudioSource(bgm, duration));
    }

    // ����� fade in, out ���
    private IEnumerator FadeInOutAudioSource(EBGM bgm, float duration = 1.5f)
    {
        if (!_bgmDic.ContainsKey(bgm))
        {
            Debug.Log("���� bgm �Դϴ�.");
            yield break;
        }

        if (_bgmAudio.clip == null)
        {
            Debug.Log("���� clip�� �����ϴ�.");
            PlayBGM(bgm);
            yield break;
        }

        if(_bgmAudio.clip == _bgmDic[bgm])
        {
            Debug.Log("������ clip�Դϴ�.");
            yield break;
        }

        float currentTime = 0;
        AudioClip clip = _bgmDic[bgm];

        // FadeOut
        while(currentTime < duration / 2)
        {
            currentTime += Time.deltaTime;
            _bgmAudio.volume = Mathf.Lerp(bgmVolume, 0, currentTime / (duration / 2));
            yield return null;
        }

        _bgmAudio.Stop();
        _bgmAudio.clip = clip;
        _bgmAudio.Play();

        // FadeIn
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            _bgmAudio.volume = Mathf.Lerp(0, bgmVolume, currentTime / duration);
            yield return null;
        }
    }
}
