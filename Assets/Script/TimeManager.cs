using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI �ؽ�Ʈ�� ������Ʈ�ϱ� ���� �ʿ�

public class TimerManager : MonoBehaviour
{
    
    private int secondsElapsed;

    void Start()
    {
        secondsElapsed = 0;
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // 1�� ���
            secondsElapsed++;
        }
    }
}
