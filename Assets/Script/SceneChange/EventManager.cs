using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class EventManager : MonoBehaviour
{
    

    public void Exit()
    {
        Debug.Log("창 닫음");

        if (NightSceneManager.Instance != null)
        {
            NightSceneManager.Instance.UnloadScene();

        }
        else
        {
            Debug.LogWarning("NightSceneManager 인스턴스를 찾을 수 없습니다.");
        }

        // "OuterMapParent" 오브젝트를 찾습니다.
        GameObject OuterMapParent = GameObject.Find("OuterMapParent");
        GameObject Nav = GameObject.Find("Nav");
        
        if (OuterMapParent == null)
        {
            // 비활성화된 오브젝트를 찾기 위해 모든 게임 오브젝트를 검색합니다.
            foreach (GameObject obj in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
            {
                if (obj.name == "OuterMapParent" && !obj.activeInHierarchy)
                {
                    OuterMapParent = obj;
                    break;
                }
            }
        }

        // 오브젝트가 존재하고 비활성화된 경우 활성화합니다.
        if (OuterMapParent != null)
        {
            if (!OuterMapParent.activeSelf)
            {
                OuterMapParent.SetActive(true);
                Debug.Log("OuterMapParent 활성화 성공");
            }
        }
        else
        {
            Debug.LogWarning("OuterMapParent 오브젝트를 찾을 수 없습니다.");
        }

        if (Nav == null)
        {
            // 비활성화된 오브젝트를 찾기 위해 모든 게임 오브젝트를 검색합니다.
            foreach (GameObject obj in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
            {
                if (obj.name == "Nav" && !obj.activeInHierarchy)
                {
                    Nav = obj;
                    break;
                }
            }
        }

        if (Nav != null)
        {
            if (!Nav.activeSelf)
            {
                Nav.SetActive(true);
                Debug.Log("OuterMapParent 활성화 성공");
            }
        }
        else
        {
            Debug.LogWarning("OuterMapParent 오브젝트를 찾을 수 없습니다.");
        }

    }

}