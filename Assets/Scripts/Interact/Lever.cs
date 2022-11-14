using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ��ȣ�ۿ� ��, �Ҵ���� ������Ʈ �ʵ带 ������ ���� ����� �ݴϴ�.
/// </summary>
public class Lever : MonoBehaviour, Interact {
    [SerializeField] UnityEvent quest;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject stick;              // ���� ��ƽ
    [SerializeField] float stickSpeed = 3f;         // ��ƽ ���� �ӵ�
    float stickTimer = 0f;                          // stick ������Ʈ ������ �ð� üũ�� ���� ������ Ÿ�̸� ����
    public bool switching = false;                  // ��ȣ�ۿ� ��, ������ ���� �Ͱ� �̴� ���� ���������ִ� ����
    bool isRotate = false;                          // �÷��̾ ��ȣ�ۿ��� ���ؼ� true�� ��ȯ ��Ű�� �ʵ� ȸ��
    bool oneShot = true;

    public bool Switching {
        get { return switching; }
    }

    void Update() {
        if (oneShot) {
            if (isRotate) {
                PullStick();
            }
            if (stickTimer >= 1f) {
                arrow.SetActive(false);
                AudioManager.instance.OneShotEvent("switchingLever");
                stickTimer = 0f;
                switching = true;
                oneShot = false;
                quest.Invoke();
            }
        }
    }
    private void PullStick() {
        stickTimer += Time.deltaTime * stickSpeed;
        stick.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(-30f, 0f, 0f), Quaternion.Euler(30f, 0f, 0f), stickTimer);

    }
    public void Work() {
        isRotate = true;
    }
}