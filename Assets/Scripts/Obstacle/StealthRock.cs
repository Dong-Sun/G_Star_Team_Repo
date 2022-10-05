using System.Collections;
using UnityEngine;

public class StealthRock : MonoBehaviour, Obstacle {
    float range = 1f;       // ������ �����ϱ� ���� ����
    float cycle = 2f;       // ���������� �ӵ��� �����ϱ� ���� ����
    Color color;            // ������Ʈ�� ������ �����ͼ� ���� �ʱ�ȭ ��Ű�� ���� ����
    bool isStealth = true;  // �ѹ��� �ϳ��� ��ƾ�� Ÿ�Բ� �ϱ� ���� �� ����
    private void Start() {
        color = GetComponent<MeshRenderer>().material.color;    // ���� ���� ������
    }
    public void Work() {
        if (isStealth) {
            isStealth = false;          // �ѹ� �� ȣ�� ��Ų��.
            StartCoroutine(Stealth());  // �ڷ�ƾ ����
        }
    }
    IEnumerator Stealth() {
        yield return null;
        while (range > 0) {     // ������ 0�̸� Ż��
            color.a = range;    // range�� color�� ������ ���ҽ�Ų��.
            GetComponent<MeshRenderer>().material.color = color;    // ���� �ʱ�ȭ
            range -= 0.01f * cycle;
            yield return new WaitForSeconds(0.01f);
        }
        gameObject.SetActive(false);    //���� ���������� ��Ȱ��ȭ
    }
}
