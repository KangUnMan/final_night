using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class login : MonoBehaviour
{
    
    // Start is called before the first frame update
    

    // Update is called once per frame
    public void LoginSceneChange() // ��ư�� ����
    {
        NightSceneManager.Instance.LoadScene("Main");
    }
}
