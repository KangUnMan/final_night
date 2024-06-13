using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleSince : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // "OuterMapParent" ������Ʈ�� ã���ϴ�.
        GameObject outerMapParent = GameObject.Find("OuterMapParent");

        // ������Ʈ�� �����ϸ� ��Ȱ��ȭ�մϴ�.
        if (outerMapParent != null)
        {
            outerMapParent.SetActive(false);
        }
        else
        {
            Debug.LogWarning("OuterMapParent ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    void OnApplicationQuit()
    {
        UserManager.Instance.SetCurrentHP(0);
    }
}
