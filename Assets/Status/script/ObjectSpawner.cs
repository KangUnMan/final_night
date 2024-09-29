using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToClone;  // ������ ������Ʈ
    public Transform spawnPoint1;  // ����� ������Ʈ�� ������ ��ġ

    private GameObject spawnedObject;
    public GameObject Prefab1;
    public GameObject Prefab2;
    public GameObject Prefab3;
    public GameObject Prefab4;
    public GameObject Prefab5;
    public GameObject Prefab6;
    public GameObject Prefab7;
    public GameObject Prefab8;
    public GameObject Prefab9;// B ������Ʈ ������
    public Transform spawnPoint; // B ������Ʈ ���� ��ġ
    public Animator animator1;
    public Animator animator2;
    public Animator animator3;
    public Animator animator4;
    public Animator animator5;
    public Animator animator6;
    public Animator animator7;
    public Animator animator8;
    public Animator animator9;
    public Canvas parentCanvas;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // B ������Ʈ ����
            spawnedObject = Instantiate(Prefab2, spawnPoint.position, spawnPoint.rotation, parentCanvas.transform);

            // C �ִϸ��̼� ���
            if (animator2 != null)
            {
                animator2.SetTrigger("PlayAnimation");
                Destroy(spawnedObject, 1.0f);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            // B ������Ʈ ����
            spawnedObject = Instantiate(Prefab3, spawnPoint.position, spawnPoint.rotation, parentCanvas.transform);

            // C �ִϸ��̼� ���
            if (animator3 != null)
            {
                animator3.SetTrigger("PlayAnimation");
                Destroy(spawnedObject, 1.0f);
            }

        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            // B ������Ʈ ����
            spawnedObject = Instantiate(Prefab4, spawnPoint.position, spawnPoint.rotation, parentCanvas.transform);

            // C �ִϸ��̼� ���
            if (animator4 != null)
            {
                animator4.SetTrigger("PlayAnimation");
                Destroy(spawnedObject, 2.0f);
            }

        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            // B ������Ʈ ����
            spawnedObject = Instantiate(Prefab5, spawnPoint.position, spawnPoint.rotation, parentCanvas.transform);

            // C �ִϸ��̼� ���
            if (animator5 != null)
            {
                animator5.SetTrigger("PlayAnimation");
                Destroy(spawnedObject, 1.0f);
            }

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            spawnedObject = Instantiate(Prefab6, spawnPoint.position, spawnPoint.rotation, parentCanvas.transform   );

            // C �ִϸ��̼� ���
            if (animator6 != null)
            {
                animator6.SetTrigger("PlayAnimation");
                Destroy(spawnedObject, 1.0f);
               
            }
        }
    }
    void CloneObject()
    {   
        Instantiate(objectToClone, spawnPoint1.position, spawnPoint1.rotation);
        
            animator6.SetTrigger("PlayAnimation");
            Destroy(objectToClone, 1.0f);


        // �ִϸ��̼� �̺�Ʈ�� ȣ��� �Լ�
    }
}