using UnityEngine;

/// <summary>
/// ��ȣ�ۿ� ��, �Ҵ���� ������Ʈ �ʵ带 ������ ���� ����� �ݴϴ�.
/// </summary>
public class Lever : MonoBehaviour, Interact {
    [SerializeField] GameObject rotateField;        // ������ ������Ʈ
    [SerializeField] float rotateAngle = 90f;       // �ѹ��� ���ư��� �� ����
    float timer = 0f;                               // �ð� üũ�� ���� ������ Ÿ�̸� ����
    bool isRotate = false;                          // �÷��̾ ��ȣ�ۿ��� ���ؼ� true�� ��ȯ ��Ű�� 
    Vector3 rotating = new Vector3(0, 1f, 0);
    void Update() {
        Rotating();
    }
    private void Rotating() {
        if (isRotate) {
            timer += Time.deltaTime;
            rotateField.transform.Rotate(rotating * rotateAngle * Time.deltaTime);
            if (timer >= 1f) {
                rotateField.transform.eulerAngles = new Vector3(0, Mathf.Round(rotateField.transform.eulerAngles.y), 0);
                rotateAngle = -rotateAngle;
                isRotate = false;
                timer = 0f;
            }
        }
    }
    public void Work() {
        Debug.Log("Lever");
        isRotate = true;
    }
}
