using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Spike���� Ȱ��ȭ ���θ� �����ϴ� ��ü�Դϴ�.
/// </summary>
public class Switch : MonoBehaviour, Interact
{
    [SerializeField] Spike[] spikes;            // ������ Spike���� ��� �迭
    [SerializeField] GameObject button;         // ��ư�� ������ ���� ������ ���ֱ� ���� ��� ��ü
    [SerializeField] bool isActive = false;     // ��ư�� �������� ���θ� üũ�ϴ� ����
    [SerializeField] float speed = 5f;          // ��ư ������ �ӵ�
    bool swap = false;                          // ��ư ���ٰ� ���Դ� �ϴ� �б⸦ ������
    float timer = 0f;                           // �ð� ����
    Vector3 start = new Vector3(0, 0.2f, 0);    // ��ư ������ �� ��ǥ
    Vector3 end = new Vector3(0, 0, 0);         // ��ư ������ �� ��ǥ
    private void Update() {
        if(isActive) {
            if(!swap) {
                PushButton();
            }
            else {
                PullButton();
            }
        }
    }
    private void PushButton() { // ��ư ����
        timer += Time.deltaTime * speed;
        button.transform.localPosition = Vector3.Lerp(start, end, timer);
        if (timer > 1f) {
            swap = !swap;
        }
    }
    private void PullButton() { // ��ư ������
        timer -= Time.deltaTime * speed;
        button.transform.localPosition = Vector3.Lerp(start, end, timer);
        if (timer < 0f) {
            swap = !swap;
            isActive = false;
        }
    }

    /// <summary>
    /// ��ư�� ������ Spike���� ������ ���� �����ݴϴ�.
    /// </summary>
    public void Work() {
        isActive = true;
        foreach (Spike spike in spikes) {   // �Ҵ� ���� ��� Spike���� Ž���ؼ� ���� ��Ȱ��ȭ
            spike.isAttack = false;
        }
    }
}