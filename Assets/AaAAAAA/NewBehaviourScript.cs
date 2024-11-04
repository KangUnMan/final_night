using UnityEngine;

public class OpacityController : MonoBehaviour
{
    public Material material;  // �Ҵ��� Material

    // ������ �����ϴ� �Լ�
    public void SetOpacity(float opacity)
    {
        Color color = material.color;
        color.a = Mathf.Clamp01(opacity); // 0���� 1 ���̷� Ŭ����
        material.color = color;
    }
}