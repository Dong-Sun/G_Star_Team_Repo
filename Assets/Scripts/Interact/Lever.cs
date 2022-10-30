using UnityEngine;

/// <summary>
/// ��ȣ�ۿ� ��, �Ҵ���� ������Ʈ �ʵ带 ������ ���� ����� �ݴϴ�.
/// </summary>
public class Lever : MonoBehaviour, Interact {
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
                AudioManager.instance.SwitchingLever();
                stickTimer = 0f;
                switching = true;
                oneShot = false;
            }
        }
    }
    private void PullStick() {
        stickTimer += Time.deltaTime * stickSpeed;
        if (!switching)
            stick.transform.localRotation
                = Quaternion.Slerp(Quaternion.Euler(-30f, 0f, 0f), Quaternion.Euler(30f, 0f, 0f), stickTimer);
        else
            stick.transform.localRotation
                = Quaternion.Slerp(Quaternion.Euler(-30f, 0f, 0f), Quaternion.Euler(30f, 0f, 0f), 1f - stickTimer);
    }
    public void Work() {
        Debug.Log("Lever");
        isRotate = true;
    }
}