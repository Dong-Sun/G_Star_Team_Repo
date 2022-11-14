using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 
/// </summary>
public class RotateLever : MonoBehaviour, Interact {
    [SerializeField] UnityEvent quest;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject rotateField;        // ������ ������Ʈ
    [SerializeField] GameObject stick;              // ���� ��ƽ
    [SerializeField] float stickSpeed = 3f;         // ��ƽ ���� �ӵ�
    [SerializeField] float fieldSpeed = 3f;         // �ʵ� ������ �ӵ�
    float stickTimer = 0f;                          // stick ������Ʈ ������ �ð� üũ�� ���� ������ Ÿ�̸� ����
    float fieldTimer = 0f;                          // rotateField ������Ʈ ������ �ð� üũ�� ���� ������ Ÿ�̸� ����
    bool isRotate = false;                          // �÷��̾ ��ȣ�ۿ��� ���ؼ� true�� ��ȯ ��Ű�� �ʵ� ȸ��
    bool rotateSound = true;
    bool oneShot = true;

    void Update() {
        if (rotateField != null) {
            if (isRotate && oneShot) {
                PullStick();
                if (stickTimer >= 1f) {
                    Rotating();
                }
            }
        }
    }
    private void PullStick() {
        stickTimer += Time.deltaTime * stickSpeed;
        stick.transform.rotation = Quaternion.Slerp(Quaternion.Euler(-30f, 0f, 0f), Quaternion.Euler(30f, 0f, 0f), stickTimer);
    }
    private void Rotating() {
        if (rotateSound) {
            rotateSound = false;
            AudioManager.instance.OneShotEvent("rotateField");
            AudioManager.instance.OneShotEvent("movingRoad");
        }
        fieldTimer += Time.deltaTime * fieldSpeed;
        rotateField.transform.rotation = Quaternion.Slerp(Quaternion.Euler(0f, 0f, 0f), Quaternion.Euler(0f, 90f, 0f), fieldTimer);
        if (fieldTimer >= 1f) {
            oneShot = false;
            arrow.SetActive(false);
            quest.Invoke();
        }
    }
    public void Work() {
        Debug.Log("Lever");
        isRotate = true;
    }
}