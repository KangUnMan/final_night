using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class EventManager : MonoBehaviour
{
    
    public void Exit()
    {
        Debug.Log("â ����");
        NightSceneManager.Instance.UnloadScene();
    }
}