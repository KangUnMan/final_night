using UnityEngine;

public class animation2 : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();  // Animator ������Ʈ�� �����ɴϴ�.
    }

    void Update()
    {
        // 'a' Ű�� ������ �ִϸ��̼� Ʈ���� ����
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("a");  // 'a'��� Ʈ���Ÿ� Ȱ��ȭ�մϴ�.
            Debug.Log("'a' �ִϸ��̼� ���!");
            animator.SetTrigger("back");
        }
    }
}
