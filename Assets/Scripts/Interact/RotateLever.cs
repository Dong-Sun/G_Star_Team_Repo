using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RotateLever : MonoBehaviour, Interact {
    [SerializeField] GameObject rotateField;        // ������ ������Ʈ
    [SerializeField] GameObject stick;              // ���� ��ƽ
    [SerializeField] float stickSpeed = 3f;         // ��ƽ ���� �ӵ�
    [SerializeField] float fieldSpeed = 3f;         // �ʵ� ������ �ӵ�
    float stickTimer = 0f;                          // stick ������Ʈ ������ �ð� üũ�� ���� ������ Ÿ�̸� ����
    float fieldTimer = 0;                           // rotateField ������Ʈ ������ �ð� üũ�� ���� ������ Ÿ�̸� ����
    public bool switching = false;                  // ��ȣ�ۿ� ��, ������ ���� �Ͱ� �̴� ���� ���������ִ� ����
    bool isRotate = false;                          // �÷��̾ ��ȣ�ۿ��� ���ؼ� true�� ��ȯ ��Ű�� �ʵ� ȸ��

    public bool Switching {
        get { return switching; }
    }

    void Update() {
        if (rotateField != null) {
            if (isRotate) {
                PullStick();
                if (stickTimer >= 1f) {
                    Rotating();
                }
            }
        }
    }
    private void PullStick() {
        stickTimer += Time.deltaTime * stickSpeed;
        if (!switching)
            stick.transform.rotation
                = Quaternion.Slerp(Quaternion.Euler(-30f, 0f, 0f), Quaternion.Euler(30f, 0f, 0f), stickTimer);
        else
            stick.transform.rotation
                = Quaternion.Slerp(Quaternion.Euler(-30f, 0f, 0f), Quaternion.Euler(30f, 0f, 0f), 1f - stickTimer);
    }
    private void Rotating() {
        fieldTimer += Time.deltaTime * fieldSpeed;
        if (!switching)
            rotateField.transform.rotation
                = Quaternion.Slerp(Quaternion.Euler(0f, 0f, 0f), Quaternion.Euler(0f, 90f, 0f), fieldTimer);
        else
            rotateField.transform.rotation
                = Quaternion.Slerp(Quaternion.Euler(0f, 0f, 0f), Quaternion.Euler(0f, 90f, 0f), 1 - fieldTimer);
        if (fieldTimer >= 1f) {
            isRotate = false;
            fieldTimer = 0f;
            stickTimer = 0f;
            switching = !switching;
        }
    }
    public void Work() {
        Debug.Log("Lever");
        isRotate = true;
    }
}