using UnityEngine;
using System.Collections;

public class FadeOutEffect : MonoBehaviour
{
    

    private SpriteRenderer spriteRenderer; // SpriteRenderer ������Ʈ
    public float duration = 2f; // ���̵�ƿ��� �ɸ��� �ð� (��)

    private void Start()
    {
        // SpriteRenderer ������Ʈ�� ������
        spriteRenderer = GetComponent<SpriteRenderer>();

        // SpriteRenderer�� ���� ��� ��� ���
        if (spriteRenderer == null)
        {
            Debug.LogWarning("�� ���� ������Ʈ���� SpriteRenderer�� �����ϴ�.");
            return;
        }

        // ���̵�ƿ� ȿ�� ����
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        // �ʱ� ����� ���� ���� ������
        Color color = spriteRenderer.color;
        float startAlpha = color.a;

        // ���̵�ƿ� ȿ�� ����
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            // ���������� Alpha ���� ����
            color.a = Mathf.Lerp(startAlpha, 0, t / duration);
            spriteRenderer.color = color;
            yield return null;
        }

        // ���� Alpha ���� 0���� ����
        color.a = 0;
        spriteRenderer.color = color;
    }
}
