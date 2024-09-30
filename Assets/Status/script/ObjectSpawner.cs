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
    public GameObject Prefab9;
    public GameObject Prefab10;
    public GameObject Prefab11;
    public GameObject Prefab12;// B ������Ʈ ������
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
    public Animator animator10;
    public Animator animator11;
    public Canvas parentCanvas;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // B ������Ʈ ����
            spawnedObject = Instantiate(Prefab1, spawnPoint.position, spawnPoint.rotation, parentCanvas.transform);

            // C �ִϸ��̼� ���
            if (animator1 != null)
            {
                animator1.SetTrigger("PlayAnimation");
                Destroy(spawnedObject, 1.0f);
            }

        }
        if (Input.GetKeyDown(KeyCode.S))
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
        if (Input.GetKeyDown(KeyCode.H))
        {
            spawnedObject = Instantiate(Prefab6, spawnPoint.position, spawnPoint.rotation, parentCanvas.transform   );

            // C �ִϸ��̼� ���
            if (animator6 != null)
            {
                animator6.SetTrigger("PlayAnimation");
                Destroy(spawnedObject, 1.0f);
               
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            spawnedObject = Instantiate(Prefab7, spawnPoint.position, spawnPoint.rotation, parentCanvas.transform);

            // C �ִϸ��̼� ���
            if (animator7 != null)
            {
                animator7.SetTrigger("PlayAnimation");
                Destroy(spawnedObject, 1.0f);

            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            spawnedObject = Instantiate(Prefab8, spawnPoint.position, spawnPoint.rotation, parentCanvas.transform);

            // C �ִϸ��̼� ���
            if (animator8 != null)
            {
                animator8.SetTrigger("PlayAnimation");
                Destroy(spawnedObject, 1.0f);

            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            spawnedObject = Instantiate(Prefab9, spawnPoint.position, spawnPoint.rotation, parentCanvas.transform);

            // C �ִϸ��̼� ���
            if (animator9 != null)
            {
                animator9.SetTrigger("PlayAnimation");
                Destroy(spawnedObject, 1.0f);

            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            spawnedObject = Instantiate(Prefab10, spawnPoint.position, spawnPoint.rotation, parentCanvas.transform);

            // C �ִϸ��̼� ���
            if (animator10 != null)
            {
                animator10.SetTrigger("PlayAnimation");
                Destroy(spawnedObject, 1.0f);

            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            spawnedObject = Instantiate(Prefab11, spawnPoint.position, spawnPoint.rotation, parentCanvas.transform);

            // C �ִϸ��̼� ���
            if (animator11 != null)
            {
                animator11.SetTrigger("PlayAnimation");
                Destroy(spawnedObject, 1.0f);

            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            spawnedObject = Instantiate(Prefab12, spawnPoint.position, spawnPoint.rotation, parentCanvas.transform);

            // C �ִϸ��̼� ���
            if (animator11 != null)
            {
                animator11.SetTrigger("PlayAnimation");
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
